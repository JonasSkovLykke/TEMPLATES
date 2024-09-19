using Domain.Common.Persistence;
using Domain.UserAggregate;
using Domain.UserAggregate.Errors;
using Domain.UserAggregate.Repositories;
using ErrorOr;
using SharedKernel.Abstractions;
using SharedKernel.ValueObjects;

namespace Application.Users.Commands.CreateUser;

internal sealed class CreateUserCommandHandler(
    IIdentityUserRepository _identityUserRepository,
    IUserRepository _userRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<CreateUserCommand, User>
{
    public async Task<ErrorOr<User>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        if (await _identityUserRepository.ExistsByEmailAsync(command.Email))
        {
            return UserErrors.DuplicateEmail;
        }

        var emailResult = Email.Create(command.Email);
        if (emailResult.IsError)
        {
            return emailResult.Errors;
        }

        var phoneNumber = PhoneNumber.Create(command.PhoneNumber);

        if (await _identityUserRepository.CreateIdentityUserAsync(emailResult.Value.Value, command.Password, phoneNumber.Value) is not int identityUserId)
        {
            return UserErrors.InvalidCredentials;
        }        

        var user = User.Create(
            identityUserId,
            emailResult.Value,
            phoneNumber,
            command.FirstName,
            command.LastName);

        await _userRepository.AddUserAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user;
    }
}
