using Domain.Common.Persistence;
using Domain.UserAggregate;
using Domain.UserAggregate.Errors;
using Domain.UserAggregate.Repositories;
using Domain.UserAggregate.ValueObjects;
using ErrorOr;
using SharedKernel.Abstractions;
using SharedKernel.Services;
using SharedKernel.ValueObjects;

namespace Application.Users.Commands.UpdateUser;

internal sealed class UpdateUserCommandHandler(
    IUserRepository _userRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<UpdateUserCommand, User>
{
    public async Task<ErrorOr<User>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var userId = StronglyTypeIdProvider.ConvertToStronglyTypedId<UserId>(command.Id);
        if (userId is null || await _userRepository.GetByIdAsync(userId!, cancellationToken) is not User user)
        {
            return UserErrors.NotFound;
        }

        var emailResult = Email.Create(command.Email);
        if (emailResult.IsError)
        {
            return emailResult.Errors;
        }

        var phoneNumber = PhoneNumber.Create(command.PhoneNumber);

        user.Update(
           emailResult.Value,
           phoneNumber,
           command.FirstName,
           command.LastName,
           command.Roles);

        _userRepository.UpdateUser(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user;
    }
}
