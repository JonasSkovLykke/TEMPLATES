using API;
using API.Controllers.Authentication;
using API.Extensions;
using Application;
using Asp.Versioning.ApiExplorer;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Serilog;

var allowSpecificOrigins = "_allowSpecificOrigins";

// Create a WebApplication builder.
var builder = WebApplication.CreateBuilder(args);
{
    // Configure and add services to the application.
    builder.Services
        .AddPresentation() // Add services for the presentation layer.
        .AddApplication() // Add services for the application layer.
        .AddInfrastructure(builder.Configuration); // Add services for the infrastructure layer.

    // Configure Serilog for logging.
    builder.Host.UseSerilog((context, configuration) =>
        configuration.ReadFrom.Configuration(context.Configuration));

    // Configure CORS (Cross-Origin Resource Sharing) policy.
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: allowSpecificOrigins,
            policy =>
            {
                policy
                .AllowAnyOrigin() // Allow requests from any origin.
                .AllowAnyMethod() // Allow all HTTP methods (GET, POST, PUT, DELETE, etc.).
                .AllowAnyHeader(); // Allow all headers in the request.
            });
    });
}

// Build the application.
var app = builder.Build();
{
    // In non-production environments, configure additional features.
    if (!app.Environment.IsProduction())
    {
        var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        // Enable Swagger API documentation.
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                // Configure Swagger endpoints for each API version.
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        });

        // Apply database migrations.
        //app.ApplyMigrations();

        // Enable request logging with Serilog. Note that this can also be useful in production.
        app.UseSerilogRequestLogging();
    }

    // Configure CORS(Cross-Origin Resource Sharing)
    app.UseCors(allowSpecificOrigins);

    // Map the Identity API for authentication.
    app.MapGroup("/Authentication")
        .AuthenticationController<IdentityUser<int>>(); // Map Identity API for IdentityUser with int as the key.

    // Configure error handling, HTTPS redirection, authorization, and controller mapping.
    app.UseExceptionHandler(); // Configure global exception handling.
    app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS.
    app.UseAuthorization(); // Enable authorization.

    if (app.Environment.IsDevelopment())
    {
        app.MapControllers().AllowAnonymous(); // Allow anonymous access to controllers in development.
    }
    else
    {
        app.MapControllers(); // Map controllers in other environments.
    }

    // Start the application.
    app.Run();
}
