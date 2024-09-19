using Application.Users.Common;
using SharedKernel.Abstractions;

namespace Application.Users.Queries.GetUser;

public sealed record GetUserQuery(Guid Id) : IQuery<UserResult>;
