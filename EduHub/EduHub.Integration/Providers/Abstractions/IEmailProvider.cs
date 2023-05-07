namespace EduHub.Integration.Providers.Abstractions;

public interface IEmailProvider
{
    Task SendEmailAsync(
       string email,
       string subject,
       string content,
       bool fromAutomatedMail = true,
       bool isHtmlContent = false,
       string? replyTo = null,
       CancellationToken cancellationToken = default
   );
}
