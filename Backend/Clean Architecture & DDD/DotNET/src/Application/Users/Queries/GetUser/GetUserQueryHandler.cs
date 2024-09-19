using Application.Users.Common;
using Domain.UserAggregate;
using Domain.UserAggregate.Errors;
using Domain.UserAggregate.Repositories;
using Domain.UserAggregate.ValueObjects;
using ErrorOr;
using SharedKernel.Abstractions;
using SharedKernel.Services;

namespace Application.Users.Queries.GetUser;

internal sealed class GetUserQueryHandler(
    IIdentityUserRepository _identityUserRepository,
    IUserRepository _userRepository) : IQueryHandler<GetUserQuery, UserResult>
{
    public async Task<ErrorOr<UserResult>> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        var userId = StronglyTypeIdProvider.ConvertToStronglyTypedId<UserId>(query.Id);
        if (userId is null || await _userRepository.GetByIdAsync(userId, cancellationToken) is not User user)
        {
            return UserErrors.NotFound;
        }

        var roles = await _identityUserRepository.GetUserRolesAsync(user.IdentityUserId);

        return new UserResult(
            user,
            roles);
    }
}
