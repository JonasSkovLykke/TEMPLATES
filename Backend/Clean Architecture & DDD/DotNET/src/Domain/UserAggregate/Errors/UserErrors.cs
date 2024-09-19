using ErrorOr;

namespace Domain.UserAggregate.Errors;

public static class UserErrors
{
    public static readonly Error DuplicateEmail = Error.Conflict(
        code: "User.DuplicateEmail",
        description: "Email is already in use.");

    public static readonly Error InvalidCredentials = Error.Validation(
        code: "User.InvalidCredentials",
        description: "Invalid Credentials.");

    public static readonly Error NotFound = Error.NotFound(
        code: "User.NotFound",
        description: "The user is not found.");
}
