using EduHub.Domain.Settings;
using EduHub.Integration.Providers.Abstractions;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace EduHub.Integration.Providers.Realizations;

public class EmailProvider: IEmailProvider
{
    private readonly SendGridSettings _sendGridSettings;

    public EmailProvider(SendGridSettings sendGridSettings) => _sendGridSettings = sendGridSettings;

    public async Task SendEmailAsync(
        string email,
        string subject,
        string content,
        bool fromAutomatedMail = true,
        bool isHtmlContent = false,
        string? replyTo = null,
        CancellationToken cancellationToken = default
    )
    {
        var apiKey = _sendGridSettings.ApiKey;
        var fromEmail = fromAutomatedMail ? _sendGridSettings.AutomatedEmail : _sendGridSettings.MessagingEmail;
        var fromDisplayName = _sendGridSettings.FromDisplayName;

        var client = new SendGridClient(apiKey);
        var from = new EmailAddress(fromEmail, fromDisplayName);
        var to = new EmailAddress(email);

        var message = isHtmlContent
            ? MailHelper.CreateSingleEmail(from, to, subject, null, content)
            : MailHelper.CreateSingleEmail(from, to, subject, content, null);

        if (replyTo != null && replyTo != "")
        {
            message.ReplyTo = new EmailAddress(replyTo);
        }

        var result = await client.SendEmailAsync(message, cancellationToken);

        if (!result.IsSuccessStatusCode) throw new Exception("Email provider is unavailabe");
       
    }
}
