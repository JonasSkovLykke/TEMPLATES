using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

/// <summary>
/// Extension methods for applying database migrations.
/// </summary>
public static class MigrationExtensions
{
    /// <summary>
    /// Apply database migrations for the AuthenticationDbContext and DotNETDbContext.
    /// </summary>
    /// <param name="app">The WebApplication instance.</param>
    public static void ApplyMigrations(this WebApplication app)
    {
        // Create a scope to access services.
        using var scope = app.Services.CreateScope();

        // Get the AuthenticationDbContext and DotNETDbContext from the service provider.
        var authenticationDbContext = scope.ServiceProvider.GetRequiredService<AuthenticationDbContext>();
        var dbContext = scope.ServiceProvider.GetRequiredService<DotNETDbContext>();

        // Apply migrations for the AuthenticationDbContext and DotNETDbContext.
        authenticationDbContext.Database.Migrate();
        dbContext.Database.Migrate();
    }
}
