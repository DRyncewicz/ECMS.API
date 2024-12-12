namespace ecms.Infrastructure.Services.EmailService;

public class EmailResponse
{
    public int ErrorCode { get; set; }

    public string ErrorMessage { get; set; } = string.Empty;

    public bool IsSuccess { get; set; }
}