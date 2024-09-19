using Domain.UserAggregate;
using SharedKernel.Abstractions;

namespace Application.Users.Commands.UpdateUser;

public sealed record UpdateUserCommand(
    Guid Id,
    string Email,
    string PhoneNumber,
    string FirstName,
    string LastName,
    ICollection<int>? Roles) : ICommand<User>;
