namespace ecms.Domain.Enums;

public enum MessageStatusType
{
    Queued = 1,
    Sent = 2,
    ConfigurationError = 3,
    AuthorizedError = 4,
    ServerError = 5,
}