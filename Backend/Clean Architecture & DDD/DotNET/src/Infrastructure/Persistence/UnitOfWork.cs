using Application.Common.Interfaces;
using Domain.Common.Persistence;
using Infrastructure.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SharedKernel.Interfaces;
using SharedKernel.Primitives;

namespace Infrastructure.Persistence;

/// <summary>
/// Represents a unit of work for managing database changes and other operations.
/// </summary>
/// <summary>
/// Initializes a new instance of the <see cref="UnitOfWork"/> class.
/// </summary>
/// <param name="dbContext">The database context.</param>
/// <param name="user">The current user.</param>
/// <param name="dateTimeProvider">The date-time provider.</param>
internal sealed class UnitOfWork(
    DotNETDbContext _dbContext,
    IUser _user,
    IDateTimeProvider _dateTimeProvider) : IUnitOfWork
{
    /// <summary>
    /// Asynchronously saves changes to the database.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous save operation.</returns>
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ConvertDomainEventsToOutboxMessages();
        UpdateAuditableEntities();

        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Converts domain events to outbox messages and adds them to the database context.
    /// </summary>
    private void ConvertDomainEventsToOutboxMessages()
    {
        var outboxMessages = _dbContext.ChangeTracker
            .Entries<IHasDomainEvents>()
            .Select(x => x.Entity)
            .SelectMany(aggregateRoot =>
            {
                var domainEvents = aggregateRoot.GetDomainEvents();

                aggregateRoot.ClearDomainEvents();

                return domainEvents;
            })
            .Select(domainEvent => new OutboxMessage
            {
                Id = Guid.NewGuid(),
                OccurredOnDateTimeOffset = _dateTimeProvider.Now,
                Type = domainEvent.GetType().Name,
                Content = JsonConvert.SerializeObject(
                    domainEvent,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    })
            })
            .ToList();

        _dbContext.OutboxMessages.AddRange(outboxMessages);
    }

    /// <summary>
    /// Updates auditable entities with creation and modification information.
    /// </summary>
    private void UpdateAuditableEntities()
    {
        var entries = _dbContext
            .ChangeTracker
            .Entries<IAuditableEntity<int?>>();

        foreach (var entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(a => a.CreatedDateTimeOffset)
                    .CurrentValue = _dateTimeProvider.Now;
                entityEntry.Property(a => a.CreatedBy)
                    .CurrentValue = _user.Id;
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(a => a.UpdatedDateTimeOffset)
                    .CurrentValue = _dateTimeProvider.Now;
                entityEntry.Property(a => a.UpdatedBy)
                    .CurrentValue = _user.Id;
            }
        }
    }
}
