using API.Common.Errors;
using API.Common.Mapping;
using API.Common.Services;
using API.Swagger;
using Application.Common.Interfaces;
using Asp.Versioning;
using Infrastructure.Authentication;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API;

/// <summary>
/// A static class for configuring dependency injection in the presentation layer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds presentation-related services to the service collection.
    /// </summary>
    /// <param name="services">The service collection to which services are added.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        // Configure API versioning with specified options.
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version"));
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });

        // Add controllers for handling HTTP requests.
        services.AddControllers();

        // Add support for ProblemDetails.
        services.AddProblemDetails();

        // Add HttpContextAccessor for accessing HTTP context.
        services.AddHttpContextAccessor();

        // Configure and add authentication and authorization services.
        services.AddAuthenticationAuthorization();

        // Add services for API endpoint exploration and Swagger documentation.
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // Configure Swagger options and custom problem details factory.
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSingleton<ProblemDetailsFactory, CustomProblemDetailsFactory>();

        // Add current user service for user-related information.
        services.AddScoped<IUser, CurrentUser>();

        // Add mappings for object-to-object transformations.
        services.AddMappings();

        return services;
    }

    /// <summary>
    /// Configures authentication and authorization services.
    /// </summary>
    /// <param name="services">The service collection to which services are added.</param>
    /// <returns>The updated service collection.</returns>
    private static IServiceCollection AddAuthenticationAuthorization(this IServiceCollection services)
    {
        // Configure and add Bearer token authentication.
        services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);

        // Add authorization services.
        services.AddAuthorization();

        // Configure and add Identity with Entity Framework store for user and role management.
        services.AddIdentityCore<IdentityUser<int>>()
            .AddRoles<IdentityRole<int>>()
            .AddEntityFrameworkStores<AuthenticationDbContext>()
            .AddApiEndpoints();

        // Add service for sending emails to users.
        services.AddSingleton<IEmailSender<IdentityUser<int>>, EmailSender>();

        return services;
    }
}
