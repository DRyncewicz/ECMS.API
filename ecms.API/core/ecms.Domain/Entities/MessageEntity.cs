using ecms.Domain.Enums;
using ecms.Domain.ValueObjects;
using SharedKernel;

namespace ecms.Domain.Entities;

public class MessageEntity : Entity
{
    public string Address { get; set; }

    public string Content { get; set; }

    public Price Subject { get; set; }

    public DateTimeOffset SentDateTimeUtc { get; set; }

    public MessageStatusType MessageStatus { get; set; }

    public int? ErrorCode { get; set; }

    public string? ErrorMessage { get; set; }

    public int ErrorAttempts { get; set; }

    public DateTimeOffset CreateDateTimeUtc { get; set; }
}
