using Domain.UserAggregate;

namespace Application.Users.Common;

public sealed record UserResult(
    User User,
    IEnumerable<string>? Roles);
