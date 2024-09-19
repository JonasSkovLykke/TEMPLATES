using MediatR;
using SharedKernel.Primitives;

namespace SharedKernel.Abstractions;

/// <summary>
/// An interface for handling domain events of a specific type.
/// </summary>
/// <typeparam name="TEvent">The type of domain event to handle.</typeparam>
public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}
