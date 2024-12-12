using ecms.Domain.Entities;
using FluentEmail.Core;
using FluentEmail.Smtp;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;

namespace ecms.Infrastructure.Services.EmailService;

public class EmailService(IOptions<SmtpOptions> _smtpOptions) : IEmailService
{
    private readonly SmtpOptions _smtpOptions = _smtpOptions.Value;

    public async Task<EmailResponse> SendEmailAsync(MessageEntity message)
    {
        var email = Email.From(_smtpOptions.FromAddress)
                         .To(message.Address)
                         .Subject(message.Subject)
                         .Body(message.Content, true);

        email.Sender = new SmtpSender(() => new SmtpClient(_smtpOptions.Host)
        {
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(_smtpOptions.UserName, _smtpOptions.Password),
            EnableSsl = true,
            Port = _smtpOptions.Port,
        });

        Email.DefaultSender = email.Sender;
        try
        {
            var response = await email.SendAsync();
            return new EmailResponse()
            {
                IsSuccess = response.Successful,
                ErrorCode = 0,
                ErrorMessage = string.Empty
            };
        }
        catch (Exception ex)
        {
            switch (ex.InnerException)
            {
                case SocketException:
                    return new EmailResponse()
                    {
                        ErrorCode = 400,
                        ErrorMessage = ex.Message.Substring(0, 150),
                        IsSuccess = false
                    };

                default:
                    return new EmailResponse()
                    {
                        ErrorCode = 500,
                        ErrorMessage = ex.Message.Substring(0, 150),
                        IsSuccess = false
                    };
            }
        }
    }
}