using ErrorOr;
using SharedKernel.Email.Errors;
using SharedKernel.Primitives;

namespace SharedKernel.Email;

/// <summary>
/// Represents an email data entity.
/// </summary>
public sealed class EmailData : Entity<Guid>
{
    // Private list to store attachments
    private readonly List<EmailAttachment> _attachments = new();

    // Sender information
    public string? FromEmail { get; init; }
    public string? FromName { get; init; }

    // Recipients
    public List<string>? To { get; init; }
    public List<string>? Bcc { get; init; }
    public List<string>? Cc { get; init; }

    // Email details
    public string? Subject { get; init; }
    public string? HtmlContent { get; init; }
    public string? TextContent { get; init; }
    public string? ReplyToEmail { get; init; }
    public string? ReplyToName { get; init; }

    // Read-only property to access attachments
    public IReadOnlyList<EmailAttachment> Attachments => _attachments.ToList();

    // Email template information
    public long? TemplateId { get; init; }

    // Scheduled sending time
    public DateTime? ScheduledAt { get; init; }

    // Private constructor to enforce creation via factory method
    private EmailData(
        Guid id,
        string? fromEmail,
        string? fromName,
        List<string>? to,
        List<string>? bcc,
        List<string>? cc,
        string? subject,
        string? htmlContent,
        string? textContent,
        string? replyToEmail,
        string? replyToName,
        long? templateId,
        DateTime? scheduledAt)
    {
        Id = id;
        FromEmail = fromEmail;
        FromName = fromName;
        To = to;
        Bcc = bcc;
        Cc = cc;
        Subject = subject;
        HtmlContent = htmlContent;
        TextContent = textContent;
        ReplyToEmail = replyToEmail;
        ReplyToName = replyToName;
        TemplateId = templateId;
        ScheduledAt = scheduledAt;
    }

    /// <summary>
    /// Factory method to create an instance of EmailData.
    /// </summary>
    /// <param name="fromEmail">Sender's email address.</param>
    /// <param name="fromName">Sender's name.</param>
    /// <param name="to">List of recipients.</param>
    /// <param name="bcc">List of blind carbon copy recipients.</param>
    /// <param name="cc">List of carbon copy recipients.</param>
    /// <param name="subject">Email subject.</param>
    /// <param name="htmlContent">HTML content of the email.</param>
    /// <param name="textContent">Text content of the email.</param>
    /// <param name="replyToEmail">Email address for replies.</param>
    /// <param name="replyToName">Name for replies.</param>
    /// <param name="templateId">ID of the email template.</param>
    /// <param name="scheduledAt">Scheduled sending time.</param>
    /// <returns>An ErrorOr instance containing either the created EmailData or an error.</returns>
    public static ErrorOr<EmailData> Create(
        string? fromEmail = null,
        string? fromName = null,
        List<string>? to = null,
        List<string>? bcc = null,
        List<string>? cc = null,
        string? subject = null,
        string? htmlContent = null,
        string? textContent = null,
        string? replyToEmail = null,
        string? replyToName = null,
        long? templateId = null,
        DateTime? scheduledAt = null)
    {
        // Check if the subject is empty
        if (subject == string.Empty)
        {
            return EmailErrors.SubjectMissing;
        }

        // Check if either HTML content or text content is provided
        if (!string.IsNullOrEmpty(htmlContent) || !string.IsNullOrEmpty(textContent))
        {
            return new EmailData(
                Guid.NewGuid(),
                fromEmail,
                fromName,
                to,
                bcc,
                cc,
                subject,
                htmlContent,
                textContent,
                replyToEmail,
                replyToName,
                templateId,
                scheduledAt);
        }
        else
        {
            return EmailErrors.ContentMissing;
        }
    }

    /// <summary>
    /// Adds an attachment to the email.
    /// </summary>
    /// <param name="attachmentUrl">URL of the attachment.</param>
    /// <param name="content">Content of the attachment.</param>
    /// <param name="attachmentName">Name of the attachment.</param>
    public void AddAttachments(
        string? attachmentUrl = null,
        byte[]? content = null,
        string? attachmentName = null)
    {
        var attachment = EmailAttachment.Create(
                attachmentUrl,
                content,
                attachmentName);

        _attachments.Add(attachment);
    }

    /// <summary>
    /// Removes an attachment from the email.
    /// </summary>
    /// <param name="attachment">Attachment to be removed.</param>
    public void RemoveAttachments(EmailAttachment attachment)
    {
        _attachments.Remove(attachment);
    }
}
