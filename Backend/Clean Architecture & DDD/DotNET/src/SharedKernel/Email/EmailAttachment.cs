namespace SharedKernel.Email;

/// <summary>
/// Represents an attachment in an email.
/// </summary>
public sealed class EmailAttachment
{
    /// <summary>
    /// Gets or sets the URL of the attachment.
    /// </summary>
    public string? AttachmentUrl { get; init; }

    /// <summary>
    /// Gets or sets the content of the attachment.
    /// </summary>
    public byte[]? Content { get; init; }

    /// <summary>
    /// Gets or sets the name of the attachment.
    /// </summary>
    public string? AttachmentName { get; init; }

    /// <summary>
    /// Private constructor to create an instance of EmailAttachment.
    /// </summary>
    /// <param name="attachmentUrl">The URL of the attachment.</param>
    /// <param name="content">The content of the attachment.</param>
    /// <param name="attachmentName">The name of the attachment.</param>
    private EmailAttachment(
        string? attachmentUrl,
        byte[]? content,
        string? attachmentName)
    {
        AttachmentUrl = attachmentUrl;
        Content = content;
        AttachmentName = attachmentName;
    }

    /// <summary>
    /// Factory method to create an instance of EmailAttachment.
    /// </summary>
    /// <param name="attachmentUrl">The URL of the attachment.</param>
    /// <param name="content">The content of the attachment.</param>
    /// <param name="attachmentName">The name of the attachment.</param>
    /// <returns>An instance of EmailAttachment.</returns>
    public static EmailAttachment Create(
        string? attachmentUrl,
        byte[]? content,
        string? attachmentName)
    {
        return new EmailAttachment(
            attachmentUrl,
            content,
            attachmentName);
    }
}
