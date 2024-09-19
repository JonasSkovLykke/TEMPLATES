using Domain.Common.Persistence;
using Domain.UserAggregate;
using Domain.UserAggregate.Errors;
using Domain.UserAggregate.Repositories;
using Domain.UserAggregate.ValueObjects;
using ErrorOr;
using SharedKernel.Abstractions;
using SharedKernel.Services;

namespace Application.Users.Commands.DeleteUser;

internal sealed class DeleteUserCommandHandler(
    IUserRepository _userRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<DeleteUserCommand, Deleted>
{
    public async Task<ErrorOr<Deleted>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var userId = StronglyTypeIdProvider.ConvertToStronglyTypedId<UserId>(command.Id);
        if (userId is null || await _userRepository.GetByIdAsync(userId, cancellationToken) is not User user)
        {
            return UserErrors.NotFound;
        }

        _userRepository.DeleteUser(user);

        user.Delete();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Deleted;
    }
}
