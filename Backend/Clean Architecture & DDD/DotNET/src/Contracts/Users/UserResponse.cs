namespace Contracts.Users;

public record UserResponse(
    Guid Id,
    string Email,
    string PhoneNumber,
    string FirstName,
    string LastName);

public sealed record UserWithRolesResponse : UserResponse
{
    public IEnumerable<string> Roles { get; init; }

    public UserWithRolesResponse(
        Guid id,
        string email,
        string phoneNumber,
        string firstName,
        string lastName,
        IEnumerable<string> roles)
        : base(
            id,
            email,
            phoneNumber,
            firstName,
            lastName)
    {
        Roles = roles;
    }
}
