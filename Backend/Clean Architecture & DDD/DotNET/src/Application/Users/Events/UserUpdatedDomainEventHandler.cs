using Domain.UserAggregate.Events;
using Domain.UserAggregate.Repositories;
using SharedKernel.Abstractions;

namespace Application.Users.Events;

internal sealed class UserUpdatedDomainEventHandler(IIdentityUserRepository _applicationUserRepository) : IDomainEventHandler<UserUpdatedDomainEvent>
{
    public async Task Handle(UserUpdatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        await _applicationUserRepository.UpdateIdentityUserAsync(
            domainEvent.IdentityUserId,
            domainEvent.Email.Value,
            domainEvent.PhoneNumber.Value);

        if (domainEvent.Roles is not null && domainEvent.Roles.Count is not 0)
        {
            await _applicationUserRepository.AddUserToRolesAsync(domainEvent.IdentityUserId, domainEvent.Roles);
        }
        else
        {
            await _applicationUserRepository.RemoveAllRolesFromUserAsync(domainEvent.IdentityUserId);
        }
    }
}
