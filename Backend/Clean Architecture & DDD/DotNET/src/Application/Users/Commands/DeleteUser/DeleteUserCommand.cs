using ErrorOr;
using SharedKernel.Abstractions;

namespace Application.Users.Commands.DeleteUser;

public sealed record DeleteUserCommand(Guid Id) : ICommand<Deleted>;
