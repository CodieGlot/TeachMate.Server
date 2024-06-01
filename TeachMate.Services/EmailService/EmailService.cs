using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using TeachMate.Domain;

namespace TeachMate.Services;
public class EmailService : IEmailService
{
    private readonly NetworkCredential _credential;
    private readonly EmailConfig _emailConfig;
    public EmailService(IOptions<EmailConfig> emailConfig)
    {
        _emailConfig = emailConfig.Value;
        _credential = new NetworkCredential(_emailConfig.AppEmail, _emailConfig.AppPassword);
    }
    public void SendTestEmail(AppUser user)
    {
        if (user.Email == null)
        {
            return;
        }
        MailMessage message = new MailMessage();
        message.From = new MailAddress(_emailConfig.AppEmail);
        message.Subject = "Test Email";
        message.To.Add(new MailAddress(user.Email));
        message.IsBodyHtml = true;
        message.Body = "<html><body>Test Content</body></html>";

        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = _credential,
            EnableSsl = true
        };

        smtpClient.Send(message);
    }

}
