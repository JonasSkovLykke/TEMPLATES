using SharedKernel.Primitives;
using SharedKernel.ValueObjects;

namespace Domain.UserAggregate.Events;

public sealed record UserUpdatedDomainEvent(
    Guid Id,
    int IdentityUserId,
    Email Email,
    PhoneNumber PhoneNumber,
    ICollection<int>? Roles) : IDomainEvent;
