using Application.Common.Interfaces;
using Application.Common.Interfaces.EmailService;
using Microsoft.Extensions.Options;
using SharedKernel.Email;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;

namespace Infrastructure.EmailService;

public sealed class EmailServiceBrevo : IEmailService
{
    private readonly ILoggerProvider _loggerService;
    private readonly EmailSettings _emailSettings;

    public EmailServiceBrevo(
        ILoggerProvider loggerService,
        IOptions<EmailSettings> emailSettings)
    {
        _loggerService = loggerService;
        _emailSettings = emailSettings.Value;

        Configuration.Default.ApiKey.Add("api-key", _emailSettings.ApiKey);
    }

    public void SendEmail(EmailData emailData)
    {
        try
        {
            var apiInstance = new TransactionalEmailsApi();

            var email = new SendSmtpEmailSender(emailData.FromName ?? _emailSettings.FromName, emailData.FromEmail ?? _emailSettings.FromEmail);

            List<SendSmtpEmailTo>? receivers = null;
            if (emailData.To is not null && emailData.To.Count is not 0)
            {
                receivers = [];

                foreach (var to in emailData.To)
                {
                    receivers.Add(new SendSmtpEmailTo(to));
                }
            }

            List<SendSmtpEmailBcc>? bccReceivers = null;
            if (emailData.Bcc is not null && emailData.Bcc.Count is not 0)
            {
                bccReceivers = [];

                foreach (var bcc in emailData.Bcc)
                {
                    bccReceivers.Add(new SendSmtpEmailBcc(bcc));
                }
            }

            List<SendSmtpEmailCc>? ccReceivers = null;
            if (emailData.Cc is not null && emailData.Cc.Count is not 0)
            {
                ccReceivers = [];

                foreach (var cc in emailData.Cc)
                {
                    ccReceivers.Add(new SendSmtpEmailCc(cc));
                }
            }

            SendSmtpEmailReplyTo? replyTo = null;
            if (emailData.ReplyToEmail is not null && emailData.ReplyToName is not null)
            {
                replyTo = new SendSmtpEmailReplyTo(emailData.ReplyToEmail, emailData.ReplyToName);
            }

            List<SendSmtpEmailAttachment>? attachments = null;
            if (emailData.Attachments.Count is not 0)
            {
                attachments = [];

                foreach (var attachment in emailData.Attachments)
                {
                    attachments.Add(new SendSmtpEmailAttachment(
                        attachment.AttachmentUrl,
                        attachment.Content,
                        attachment.AttachmentName));
                }
            }

            var sendSmtpEmail = new SendSmtpEmail(
                email,
                receivers,
                bccReceivers,
                ccReceivers,
                emailData.HtmlContent,
                emailData.TextContent,
                emailData.Subject,
                replyTo,
                attachments,
                null,
                emailData.TemplateId,
                null,
                null,
                null,
                emailData.ScheduledAt,
                null);

            var result = apiInstance.SendTransacEmail(sendSmtpEmail);

            _loggerService.LogInformation("Email send with ID: " + result.MessageId, nameof(EmailServiceBrevo), nameof(SendEmail));
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(EmailServiceBrevo), nameof(SendEmail));
        }
    }
}
