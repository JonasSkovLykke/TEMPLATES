namespace Infrastructure.Persistence.Outbox;

/// <summary>
/// Represents an outbox message to be sent to external systems.
/// </summary>
public sealed class OutboxMessage
{
    /// <summary>
    /// Gets or sets the unique identifier of the outbox message.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the type of the outbox message.
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the content of the outbox message.
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date and time when the outbox message occurred.
    /// </summary>
    public DateTimeOffset OccurredOnDateTimeOffset { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the outbox message was processed.
    /// </summary>
    public DateTimeOffset? ProcessedOnDateTimeOffset { get; set; }

    /// <summary>
    /// Gets or sets the error message if the processing of the outbox message failed.
    /// </summary>
    public string? Error { get; set; }
}
