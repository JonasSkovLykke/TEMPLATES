using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Swagger;

/// <summary>
/// Configuration options for Swagger generation.
/// </summary>
public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConfigureSwaggerOptions"/> class.
    /// </summary>
    /// <param name="provider">The API version description provider.</param>
    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    /// <summary>
    /// Configures SwaggerGenOptions for adding security definitions and requirements.
    /// </summary>
    /// <param name="options">The SwaggerGenOptions to configure.</param>
    public void Configure(SwaggerGenOptions options)
    {
        // Add a security definition for "Bearer" token authentication.
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Standard Authorization header using the Bearer scheme (\"Bearer {token}\")",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "Bearer",
            Scheme = "Bearer"
        });

        // Add security definition for API key
        //options.AddSecurityDefinition("APIKey", new OpenApiSecurityScheme
        //{
        //    In = ParameterLocation.Header,
        //    Description = "X-API-Key Header using the APIKey scheme (\"{APIKey}\")",
        //    Name = "X-API-Key",
        //    Type = SecuritySchemeType.ApiKey,
        //    Scheme = "APIKeyScheme"
        //});

        // Add a security requirement for "Bearer" token authentication.
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });

        // Add a security requirement for "APIKey" token authentication
        //options.AddSecurityRequirement(new OpenApiSecurityRequirement
        //{
        //    {
        //        new OpenApiSecurityScheme
        //        {
        //            Reference = new OpenApiReference
        //            {
        //                Type = ReferenceType.SecurityScheme,
        //                Id = "APIKey"
        //            },
        //            In = ParameterLocation.Header
        //        },
        //        // No scopes required for API key authentication
        //        Array.Empty<string>()
        //    }
        //});

        // Add swagger document for every API version discovered.
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(
                description.GroupName,
                CreateVersionInfo(description));
        }
    }

    /// <summary>
    /// Creates version information for Swagger document.
    /// </summary>
    /// <param name="description">The API version description.</param>
    /// <returns>The OpenApiInfo containing title and version information.</returns>
    private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
    {
        var info = new OpenApiInfo()
        {
            Title = "DotNET (.NET 8) Web API",
            Version = description.ApiVersion.ToString()
        };

        if (description.IsDeprecated)
        {
            info.Description += " This API version has been deprecated. Please use one of the new APIs available from the explorer.";
        }

        return info;
    }
}
