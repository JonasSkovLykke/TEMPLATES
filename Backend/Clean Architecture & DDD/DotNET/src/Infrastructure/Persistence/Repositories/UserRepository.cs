using Application.Common.Interfaces;
using Domain.UserAggregate;
using Domain.UserAggregate.Repositories;
using Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using SharedKernel.ValueObjects;
using System.Runtime.CompilerServices;

namespace Infrastructure.Persistence.Repositories;

internal sealed class UserRepository(
    ILoggerProvider _loggerService,
    DotNETDbContext _dbContext) : IUserRepository
{
    private void LogError(Exception ex, [CallerMemberName] string operation = "")
    {
        _loggerService.LogError(ex, nameof(UserRepository), operation);
    }

    public Task<bool> ExistsByIdAsync(UserId userId, CancellationToken cancellationToken = default)
    {
        try
        {
            return _dbContext.Users.AnyAsync(user => user.Id.Value == userId.Value, cancellationToken);
        }
        catch (Exception ex)
        {
            LogError(ex);

            throw;
        }
    }

    public Task<bool> ExistsByEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        try
        {
            return _dbContext.Users.AnyAsync(user => user.Email == email, cancellationToken);
        }
        catch (Exception ex)
        {
            LogError(ex);

            throw;
        }
    }

    public Task<User?> GetByIdAsync(UserId userId, CancellationToken cancellationToken = default)
    {
        try
        {
            return _dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId, cancellationToken);
        }
        catch (Exception ex)
        {
            LogError(ex);

            throw;
        }
    }

    public Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        try
        {
            return _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email, cancellationToken);
        }
        catch (Exception ex)
        {
            LogError(ex);

            throw;
        }
    }

    public Task<User?> GetByIdentityUserIdAsync(int identityUserId, CancellationToken cancellationToken = default)
    {
        try
        {
            return _dbContext.Users.FirstOrDefaultAsync(user => user.IdentityUserId == identityUserId, cancellationToken);
        }
        catch (Exception ex)
        {
            LogError(ex);

            throw;
        }
    }

    public Task<List<User>> GetUsersAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return _dbContext.Users.ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            LogError(ex);

            throw;
        }
    }

    public async Task AddUserAsync(User user, CancellationToken cancellationToken = default)
    {
        try
        {
            await _dbContext.Users.AddAsync(user, cancellationToken);
        }
        catch (Exception ex)
        {
            LogError(ex);

            throw;
        }
    }

    public void UpdateUser(User user)
    {
        try
        {
            _dbContext.Update(user);
        }
        catch (Exception ex)
        {
            LogError(ex);

            throw;
        }
    }

    public void DeleteUser(User user)
    {
        try
        {
            _dbContext.Remove(user);
        }
        catch (Exception ex)
        {
            LogError(ex);

            throw;
        }
    }
}
