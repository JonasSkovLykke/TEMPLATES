namespace Contracts.Users;

public sealed record UpdateUserRequest(
    string Email,
    string PhoneNumber,
    string FirstName,
    string LastName,    
    IEnumerable<int>? Roles);
