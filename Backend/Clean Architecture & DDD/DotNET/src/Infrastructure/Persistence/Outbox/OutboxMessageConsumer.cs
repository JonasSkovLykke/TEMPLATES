namespace Infrastructure.Persistence.Outbox;

/// <summary>
/// Represents a consumer of outbox messages.
/// </summary>
public sealed class OutboxMessageConsumer
{
    /// <summary>
    /// Gets or sets the unique identifier of the outbox message consumer.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the outbox message consumer.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
