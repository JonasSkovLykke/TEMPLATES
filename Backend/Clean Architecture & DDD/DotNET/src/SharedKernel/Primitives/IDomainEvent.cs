using MediatR;

namespace SharedKernel.Primitives;

/// <summary>
/// An interface representing a domain event that can be used with MediatR.
/// </summary>
public interface IDomainEvent : INotification
{
    /// <summary>
    /// Gets or sets the unique identifier of the domain event.
    /// </summary>
    Guid Id { get; init; }
}
