
namespace EduHub.Domain.Settings;

public class SendGridSettings
{
    public string HelloEmail { get; set; } = default!;

    public string ApiKey { get; set; } = default!;

    public string AutomatedEmail { get; set; } = default!;

    public string MessagingEmail { get; set; } = default!;

    public string FromDisplayName { get; set; } = default!;
}
