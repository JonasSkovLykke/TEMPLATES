using Application.Common.Interfaces;
using Domain.UserAggregate.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository implementation for managing identity-related operations using Entity Framework.
/// Initializes a new instance of the <see cref="IdentityUserRepository"/> class.
/// </summary>
/// <param name="loggerService">The logger for logging.</param>
/// <param name="userManager">The UserManager for managing user-related operations.</param>
/// <param name="roleManager">The RoleManager for managing role-related operations.</param>
internal sealed class IdentityUserRepository(
    ILoggerProvider _loggerService,
    UserManager<IdentityUser<int>> _userManager,
    RoleManager<IdentityRole<int>> _roleManager) : IIdentityUserRepository
{
    private void LogError(Exception ex, [CallerMemberName] string operation = "")
    {
        _loggerService.LogError(ex, nameof(IdentityUserRepository), operation);
    }

    #region IdentityUser
    public async Task<bool> ExistsByIdAsync(int userId)
    {
        try
        {
            return await FindByIdAsync(userId) != null;
        }
        catch (Exception ex)
        {
            LogError(ex);

            throw;
        }
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        try
        {
            return await FindByEmailAsync(email) != null;
        }
        catch (Exception ex)
        {
            LogError(ex);

            throw;
        }
    }

    private async Task<IdentityUser<int>?> FindByIdAsync(int userId)
    {
        try
        {
            return await _userManager.FindByIdAsync(userId.ToString());
        }
        catch (Exception ex)
        {
            LogError(ex);

            throw;
        }
    }

    private async Task<IdentityUser<int>?> FindByEmailAsync(string email)
    {
        try
        {
            return await _userManager.FindByEmailAsync(email);
        }
        catch (Exception ex)
        {
            LogError(ex);

            throw;
        }
    }
    
    public async Task<int?> CreateIdentityUserAsync(string email, string password, string? phoneNumber = "")
    {
        try
        {
            var identityUser = new IdentityUser<int>
            {
                Email = email,
                UserName = email,
                PhoneNumber = phoneNumber
            };

            var identityResult = await _userManager.CreateAsync(identityUser, password);

            if (identityResult.Succeeded)
            {
                return identityUser.Id;
            }
            else
            {
                var errorMessage = string.Join(", ", identityResult.Errors.Select(error => error.Description));
                var ex = new Exception(errorMessage);

                LogError(ex);
            }
        }
        catch (Exception ex)
        {
            LogError(ex);

            throw;
        }

        return null;
    }

    public async Task<bool> UpdateIdentityUserAsync(int userId, string email, string? phoneNumber = "")
    {
        try
        {
            if (await FindByIdAsync(userId) is not IdentityUser<int> identityUser)
            {
                return false;
            }

            identityUser.Email = email;
            identityUser.UserName = email;
            identityUser.PhoneNumber = phoneNumber;

            var identityResult = await _userManager.UpdateAsync(identityUser);

            return identityResult.Succeeded;
        }
        catch (Exception ex)
        {
            LogError(ex);

            throw;
        }
    }

    public async Task<bool> DeleteIdentityUserAsync(int userId)
    {
        try
        {
            if (await FindByIdAsync(userId) is not IdentityUser<int> identityUser)
            {
                return false;
            }

            var identityResult = await _userManager.DeleteAsync(identityUser);

            return identityResult.Succeeded;
        }
        catch (Exception ex)
        {
            LogError(ex);

            throw;
        }
    }
    #endregion

    #region IdentityRole
    public async Task<bool> IsUserInRoleAsync(int userId, int roleId)
    {
        try
        {
            if (await FindByIdAsync(userId) is not IdentityUser<int> identityUser)
            {
                return false;
            }

            var role = await _roleManager.FindByIdAsync(roleId.ToString());

            if (role is null)
            {
                return false;
            }

            return await _userManager.IsInRoleAsync(identityUser, role.Name!);
        }
        catch (Exception ex)
        {
            LogError(ex);

            throw;
        }
    }


    public async Task<IEnumerable<string>> GetUserRolesAsync(int userId)
    {
        try
        {
            if (await FindByIdAsync(userId) is not IdentityUser<int> identityUser)
            {
                return Enumerable.Empty<string>();
            }

            return await _userManager.GetRolesAsync(identityUser);
        }
        catch (Exception ex)
        {
            LogError(ex);

            throw;
        }
    }

    public async Task<bool> AddUserToRolesAsync(int userId, IEnumerable<int> roleIds)
    {
        try
        {
            if (await FindByIdAsync(userId) is not IdentityUser<int> identityUser)
            {
                return false;
            }

            var userRoleIds = await _roleManager.Roles
                .Where(r => roleIds.Contains(r.Id))
                .Select(r => r.Id)
                .ToListAsync();

            if (userRoleIds.Count is 0)
            {
                return false;
            }

            await RemoveAllRolesFromUserAsync(userId);

            var rolesToAddNames = await _roleManager.Roles
                .Where(r => userRoleIds.Contains(r.Id))
                .Select(r => r.Name)
                .ToListAsync();

            var result = await _userManager.AddToRolesAsync(identityUser, rolesToAddNames!);

            return result.Succeeded;
        }
        catch (Exception ex)
        {
            LogError(ex);

            throw;
        }
    }

    public async Task<bool> RemoveAllRolesFromUserAsync(int userId)
    {
        try
        {
            if (await FindByIdAsync(userId) is not IdentityUser<int> identityUser)
            {
                return false;
            }

            var userRoles = await _userManager.GetRolesAsync(identityUser);

            if (userRoles.Any())
            {
                var result = await _userManager.RemoveFromRolesAsync(identityUser, userRoles);

                return result.Succeeded;
            }

            return true; // Even if no roles were removed, it's still considered a success
        }
        catch (Exception ex)
        {
            LogError(ex);

            throw;
        }
    }
    #endregion
}
