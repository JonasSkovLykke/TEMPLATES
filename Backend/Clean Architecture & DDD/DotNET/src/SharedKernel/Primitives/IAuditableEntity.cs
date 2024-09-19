namespace SharedKernel.Primitives;

/// <summary>
/// An interface representing an entity that includes audit-related properties.
/// </summary>
public interface IAuditableEntity<TId>
{
    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// </summary>
    DateTimeOffset CreatedDateTimeOffset { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who created the entity.
    /// </summary>
    TId? CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was last updated, if applicable.
    /// </summary>
    DateTimeOffset? UpdatedDateTimeOffset { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who last updated the entity, if applicable.
    /// </summary>
    TId? UpdatedBy { get; set; }
}
