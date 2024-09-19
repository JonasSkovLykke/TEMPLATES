namespace SharedKernel.Primitives;

/// <summary>
/// An abstract base class representing an aggregate root in the domain model.
/// </summary>
/// <typeparam name="TId">The type of value used as the unique identifier for the aggregate root.</typeparam>
public abstract class AggregateRoot<TId> : Entity<TId>, IHasDomainEvents
    where TId : notnull
{
    private readonly List<IDomainEvent> _domainEvents = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateRoot{TId}"/> class with the specified unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the aggregate root.</param>
    protected AggregateRoot(TId id) : base(id)
    {
    }

    /// <summary>
    /// Gets a collection of domain events associated with this aggregate root.
    /// </summary>
    /// <returns>A read-only collection of domain events.</returns>
    public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => [.. _domainEvents];

    /// <summary>
    /// Clears the collection of domain events associated with this aggregate root.
    /// </summary>
    public void ClearDomainEvents() => _domainEvents.Clear();

    /// <summary>
    /// Raises a domain event for this aggregate root.
    /// </summary>
    /// <param name="domainEvent">The domain event to raise.</param>
    protected void RaiseDomainEvent(IDomainEvent domainEvent) =>
        _domainEvents.Add(domainEvent);

    /// <summary>
    /// Private constructor without parameters required for entity framework (EFCore) to work.
    /// </summary>
    private AggregateRoot()
    {
    }
}
