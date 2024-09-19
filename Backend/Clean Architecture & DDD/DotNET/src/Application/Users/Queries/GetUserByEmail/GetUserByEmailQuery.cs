using Application.Users.Common;
using SharedKernel.Abstractions;

namespace Application.Users.Queries.GetUserByEmail;

public sealed record GetUserByEmailQuery(string Email) : IQuery<UserResult>;
