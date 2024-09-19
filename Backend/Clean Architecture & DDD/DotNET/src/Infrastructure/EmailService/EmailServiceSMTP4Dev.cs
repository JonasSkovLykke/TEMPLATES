using Application.Common.Interfaces;
using Application.Common.Interfaces.EmailService;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SharedKernel.Email;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Infrastructure.EmailService;

public sealed class EmailServiceSMTP4Dev : IEmailService
{
    private readonly ILoggerProvider _loggerService;
    private readonly EmailSettings _emailSettings;

    public EmailServiceSMTP4Dev(
        ILoggerProvider loggerService,
        IOptions<EmailSettings> emailSettings)
    {
        _loggerService = loggerService;
        _emailSettings = emailSettings.Value;
    }

    public void SendEmail(EmailData emailData)
    {
        try
        {
            var mail = new MimeMessage();

            #region Sender / Receiver
            mail.From.Add(new MailboxAddress(emailData.FromName ?? _emailSettings.FromName, emailData.FromEmail ?? _emailSettings.FromEmail));
            mail.Sender = new MailboxAddress(emailData.FromName ?? _emailSettings.FromName, emailData.FromEmail ?? _emailSettings.FromEmail);

            if (emailData.To != null)
            {
                foreach (string mailAddress in emailData.To!)
                {
                    mail.To.Add(MailboxAddress.Parse(mailAddress));
                }
            }

            if (!string.IsNullOrEmpty(emailData.ReplyToName) && !string.IsNullOrEmpty(emailData.ReplyToEmail))
            {
                mail.ReplyTo.Add(new MailboxAddress(emailData.ReplyToName, emailData.ReplyToEmail));
            }

            if (emailData.Bcc is not null)
            {
                foreach (string mailAddress in emailData.Bcc.Where(x => !string.IsNullOrWhiteSpace(x)))
                {
                    mail.Bcc.Add(MailboxAddress.Parse(mailAddress.Trim()));
                }
            }

            if (emailData.Cc is not null)
            {
                foreach (string mailAddress in emailData.Cc.Where(x => !string.IsNullOrWhiteSpace(x)))
                {
                    mail.Cc.Add(MailboxAddress.Parse(mailAddress.Trim()));
                }
            }
            #endregion

            #region Content
            var body = new BodyBuilder();
            mail.Subject = emailData.Subject;

            if (emailData.Attachments != null)
            {
                foreach (EmailAttachment attachment in emailData.Attachments)
                {
                    body.Attachments.Add(attachment.AttachmentName, attachment.Content);
                }
            }

            body.HtmlBody = emailData.HtmlContent ?? emailData.TextContent;
            mail.Body = body.ToMessageBody();
            #endregion

            var result = SendEmail(emailData, mail);

            _loggerService.LogInformation("Email send with ID: " + result, nameof(EmailServiceSMTP4Dev), nameof(SendEmail));
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(EmailServiceSMTP4Dev), nameof(SendEmail));
        }

    }

    private string? SendEmail(EmailData emailData, MimeMessage mail, CancellationToken ct = default)
    {
        while (emailData.ScheduledAt > DateTime.UtcNow)
        {
            Thread.Sleep(60000);
        }

        using var smtp = new SmtpClient();
        smtp.Connect(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTlsWhenAvailable, ct);

        string? returnMessage = smtp.Send(mail, ct);
        smtp.Disconnect(true, ct);

        return returnMessage;
    }
}
