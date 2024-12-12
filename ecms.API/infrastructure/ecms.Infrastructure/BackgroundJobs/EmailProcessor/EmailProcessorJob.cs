using ecms.Application.Abstractions.Data;
using ecms.Domain.Entities;
using ecms.Domain.Enums;
using ecms.Infrastructure.Services.EmailService;
using Microsoft.EntityFrameworkCore;
using SharedKernal;

namespace ecms.Infrastructure.BackgroundJobs.EmailProcessor;

public class EmailProcessorJob(IApplicationDbContext _applicationDbContext,
                               IEmailService _emailService,
                               IDateTimeProvider _dateTimeProvider)
{
    public async Task ProcessAsync()
    {
        var currentUtcTime = _dateTimeProvider.UtcNow;
        var messages = await _applicationDbContext.Messages
            .Where(p => p.MessageStatus != MessageStatusType.Sent &&
                        p.SentDateTimeUtc < currentUtcTime &&
                        p.SentDateTimeUtc > currentUtcTime.AddDays(-3) &&
                        p.ErrorAttempts < 5)
            .OrderBy(p => p.SentDateTimeUtc)
            .Take(50)
            .ToListAsync();

        var messageUpdates = new List<MessageEntity>();

        var sendEmailTasks = messages.Select(async message =>
        {
            var response = await _emailService.SendEmailAsync(message);
            return (message, response);
        });

        var results = await Task.WhenAll(sendEmailTasks);

        foreach (var (message, response) in results)
        {
            if (response.IsSuccess)
            {
                message.MessageStatus = MessageStatusType.Sent;
                message.SentDateTimeUtc = currentUtcTime;
            }
            else
            {
                message.ErrorCode = response.ErrorCode;
                message.MessageStatus = MessageStatusType.ServerError;
                message.ErrorAttempts++;
                message.ErrorMessage = response.ErrorMessage;
                message.SentDateTimeUtc = currentUtcTime;
            }
            messageUpdates.Add(message);
        }

        await _applicationDbContext.SaveChangesAsync();
    }
}