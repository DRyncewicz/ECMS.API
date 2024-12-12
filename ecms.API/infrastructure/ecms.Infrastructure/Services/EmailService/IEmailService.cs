using ecms.Domain.Entities;

namespace ecms.Infrastructure.Services.EmailService;

public interface IEmailService
{
    Task<EmailResponse> SendEmailAsync(MessageEntity message);
}