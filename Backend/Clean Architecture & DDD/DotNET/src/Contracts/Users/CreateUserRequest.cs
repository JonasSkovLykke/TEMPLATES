namespace Contracts.Users;

public sealed record CreateUserRequest(
    string Email,
    string Password,
    string PhoneNumber,
    string FirstName,
    string LastName);
