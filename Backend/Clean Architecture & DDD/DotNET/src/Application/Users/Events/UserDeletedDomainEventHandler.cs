using Domain.UserAggregate.Events;
using Domain.UserAggregate.Repositories;
using SharedKernel.Abstractions;

namespace Application.Users.Events;

internal sealed class UserDeletedDomainEventHandler(IIdentityUserRepository _applicationUserRepository) : IDomainEventHandler<UserDeletedDomainEvent>
{
    public async Task Handle(UserDeletedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        await _applicationUserRepository.DeleteIdentityUserAsync(domainEvent.IdentityUserId);
    }
}
