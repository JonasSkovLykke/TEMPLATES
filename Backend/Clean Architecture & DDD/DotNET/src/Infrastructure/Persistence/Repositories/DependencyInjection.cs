using Domain.Common.Persistence;
using Domain.UserAggregate.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence.Repositories;

internal static class DependencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IIdentityUserRepository, IdentityUserRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
