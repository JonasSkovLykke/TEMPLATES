using Domain.UserAggregate;
using SharedKernel.Abstractions;

namespace Application.Users.Commands.Register;

public sealed record GetUsersQuery() : IQuery<IEnumerable<User>>;
