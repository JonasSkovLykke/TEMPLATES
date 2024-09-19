using Mapster;
using MapsterMapper;
using System.Reflection;

namespace API.Common.Mapping;

/// <summary>
/// A static class for configuring dependency injection of object mapping services.
/// </summary>
internal static class DependencyInjection
{
    /// <summary>
    /// Adds object mapping services to the service collection.
    /// </summary>
    /// <param name="services">The service collection to which services are added.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        // Create a configuration for object mapping and scan the current assembly for mappings.
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        // Register the configuration as a singleton and the IMapper as a scoped service.
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}
