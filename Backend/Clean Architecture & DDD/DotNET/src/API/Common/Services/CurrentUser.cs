using Application.Common.Interfaces;
using System.Security.Claims;

namespace API.Common.Services;

/// <summary>
/// Represents the current user service that implements the IUser interface.
/// </summary>
/// <param name="httpContextAccessor">The HTTP context accessor.</param>
public class CurrentUser(IHttpContextAccessor _httpContextAccessor) : IUser
{
    /// <summary>
    /// Gets the identifier of the current user.
    /// </summary>
    public int? Id
    {
        get
        {
            // Attempt to parse the "NameIdentifier" claim to retrieve the user ID.
            if (int.TryParse(_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier), out int id))
            {
                return id;
            }

            return null;
        }
    }
}
