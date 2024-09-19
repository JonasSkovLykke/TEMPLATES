using Domain.UserAggregate.ValueObjects;
using SharedKernel.ValueObjects;

namespace Domain.UserAggregate.Repositories;

public interface IUserRepository
{
    public Task<bool> ExistsByIdAsync(UserId userId, CancellationToken cancellationToken = default);
    public Task<bool> ExistsByEmailAsync(Email email, CancellationToken cancellationToken = default);
    public Task<User?> GetByIdAsync(UserId userId, CancellationToken cancellationToken = default);
    public Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);
    public Task<User?> GetByIdentityUserIdAsync(int identityUserId, CancellationToken cancellationToken = default);
    public Task<List<User>> GetUsersAsync(CancellationToken cancellationToken = default);
    public Task AddUserAsync(User user, CancellationToken cancellationToken = default);
    public void UpdateUser(User user);
    public void DeleteUser(User user);
}
