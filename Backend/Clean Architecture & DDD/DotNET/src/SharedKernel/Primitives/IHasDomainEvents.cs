namespace SharedKernel.Primitives;

/// <summary>
/// An interface representing an entity that can produce and manage domain events.
/// </summary>
public interface IHasDomainEvents
{
    /// <summary>
    /// Gets a collection of domain events associated with the entity.
    /// </summary>
    /// <returns>A read-only collection of domain events.</returns>
    IReadOnlyCollection<IDomainEvent> GetDomainEvents();

    /// <summary>
    /// Clears the collection of domain events associated with the entity.
    /// </summary>
    void ClearDomainEvents();
}
