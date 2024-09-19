using Domain.UserAggregate;
using SharedKernel.Abstractions;

namespace Application.Users.Commands.CreateUser;

public sealed record CreateUserCommand(
    string Email,
    string Password,
    string PhoneNumber,
    string FirstName,
    string LastName) : ICommand<User>;
