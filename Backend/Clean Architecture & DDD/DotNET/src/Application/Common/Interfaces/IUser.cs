namespace Application.Common.Interfaces;

/// <summary>
/// Represents the user information, including the user identifier.
/// </summary>
public interface IUser
{
    /// <summary>
    /// Gets the identifier of the user.
    /// </summary>
    int? Id { get; }
}
