using SharedKernel.Primitives;

namespace Domain.UserAggregate.Events;

public sealed record UserDeletedDomainEvent(
    Guid Id,
    int IdentityUserId) : IDomainEvent;
