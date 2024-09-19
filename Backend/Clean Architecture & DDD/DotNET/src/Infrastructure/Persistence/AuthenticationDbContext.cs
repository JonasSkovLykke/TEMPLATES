using Domain.UserAggregate.Entities;
using Infrastructure.Persistence.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

/// <summary>
/// Represents the database context for authentication-related entities.
/// </summary>
public sealed class AuthenticationDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
{
    public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Configures the database schema, renames Identity tables, and seeds initial roles.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("authentication");

        base.OnModelCreating(modelBuilder);

        RenameIdentityUserTablesSeedData(modelBuilder);
    }

    private static void RenameIdentityUserTablesSeedData(ModelBuilder modelBuilder)
    {
        // Rename Identity tables to custom names.
        modelBuilder.Entity<IdentityUser<int>>().ToTable(TableNames.Users);
        modelBuilder.Entity<IdentityRole<int>>().ToTable(TableNames.Roles);
        modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable(TableNames.RoleClaims);
        modelBuilder.Entity<IdentityUserClaim<int>>().ToTable(TableNames.UserClaims);
        modelBuilder.Entity<IdentityUserLogin<int>>().ToTable(TableNames.UserLogins);
        modelBuilder.Entity<IdentityUserRole<int>>().ToTable(TableNames.UserRoles);
        modelBuilder.Entity<IdentityUserToken<int>>().ToTable(TableNames.UserTokens);

        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        // Seed initial roles.
        var roles = RoleEnum.GetValues();

        foreach (var role in roles)
        {
            modelBuilder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int>
            {
                Id = role.Id,
                Name = role.Name,
                NormalizedName = role.Name.ToUpper()
            });
        }
    }
}
