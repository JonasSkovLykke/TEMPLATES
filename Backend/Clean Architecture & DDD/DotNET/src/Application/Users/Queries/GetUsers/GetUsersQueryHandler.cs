using Domain.UserAggregate;
using Domain.UserAggregate.Repositories;
using ErrorOr;
using SharedKernel.Abstractions;

namespace Application.Users.Commands.Register;

internal sealed class GetUsersQueryHandler(IUserRepository _userRepository) : IQueryHandler<GetUsersQuery, IEnumerable<User>>
{
    public async Task<ErrorOr<IEnumerable<User>>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        return await _userRepository.GetUsersAsync(cancellationToken);
    }
}
