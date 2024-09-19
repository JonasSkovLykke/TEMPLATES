namespace Domain.UserAggregate.Repositories;

public interface IIdentityUserRepository
{
    #region IdentityUser
    public Task<bool> ExistsByIdAsync(int userId);
    public Task<bool> ExistsByEmailAsync(string email);
    public Task<int?> CreateIdentityUserAsync(string email, string password, string phoneNumber);
    public Task<bool> UpdateIdentityUserAsync(int userId, string email, string phoneNumber);
    public Task<bool> DeleteIdentityUserAsync(int userId);
    #endregion

    #region IdentityRole
    public Task<bool> IsUserInRoleAsync(int userId, int roleId);
    public Task<IEnumerable<string>> GetUserRolesAsync(int userId);
    public Task<bool> AddUserToRolesAsync(int userId, IEnumerable<int> roleIds);
    public Task<bool> RemoveAllRolesFromUserAsync(int userId);
    #endregion
}
