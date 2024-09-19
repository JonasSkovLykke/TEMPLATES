using Application.Users.Common;
using Domain.UserAggregate;
using Domain.UserAggregate.Errors;
using Domain.UserAggregate.Repositories;
using ErrorOr;
using SharedKernel.Abstractions;
using SharedKernel.ValueObjects;

namespace Application.Users.Queries.GetUserByEmail;

internal sealed class GetUserByEmailQueryHandler(
    IIdentityUserRepository _identityUserRepository,
    IUserRepository _userRepository) : IQueryHandler<GetUserByEmailQuery, UserResult>
{
    public async Task<ErrorOr<UserResult>> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken)
    {
        var email = Email.Create(query.Email);
        if (await _userRepository.GetByEmailAsync(email.Value, cancellationToken) is not User user)
        {
            return UserErrors.NotFound;
        }

        var roles = await _identityUserRepository.GetUserRolesAsync(user.IdentityUserId);

        return new UserResult(
            user,
            roles);
    }
}
