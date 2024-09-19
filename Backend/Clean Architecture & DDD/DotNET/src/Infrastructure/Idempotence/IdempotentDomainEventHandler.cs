using Infrastructure.Persistence;
using Infrastructure.Persistence.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Abstractions;
using SharedKernel.Primitives;

namespace Infrastructure.Idempotence;

/// <summary>
/// An implementation of a domain event handler that ensures idempotent handling of domain events.
/// </summary>
/// <typeparam name="TDomainEvent">The type of domain event to handle.</typeparam>
public sealed class IdempotentDomainEventHandler<TDomainEvent>(
    INotificationHandler<TDomainEvent> _decorated,
    DotNETDbContext _dbContext) : IDomainEventHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    /// <summary>
    /// Handles the domain event ensuring idempotent processing.
    /// </summary>
    /// <param name="notification">The domain event to handle.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public async Task Handle(TDomainEvent notification, CancellationToken cancellationToken)
    {
        string consumer = _decorated.GetType().Name;

        if (await _dbContext.OutboxMessageConsumers.AnyAsync(outboxMessageConsumer
            => outboxMessageConsumer.Id == notification.Id
            && outboxMessageConsumer.Name == consumer,
            cancellationToken))
        {
            return;
        }

        await _decorated.Handle(notification, cancellationToken);

        _dbContext.OutboxMessageConsumers.Add(new OutboxMessageConsumer
        {
            Id = notification.Id,
            Name = consumer
        });

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
