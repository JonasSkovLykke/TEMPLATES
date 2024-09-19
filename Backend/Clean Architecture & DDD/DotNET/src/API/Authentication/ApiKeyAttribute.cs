using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Authentication;

// This attribute can be applied to classes or methods
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
internal sealed class ApiKeyAttribute : Attribute, IAuthorizationFilter
{
    // Name of the header where the API key is expected
    private const string ApiKeyHeaderName = "X-Api-Key";

    // Method executed when authorization is being verified
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Check if the provided API key is valid
        if (!IsApiKeyValid(context.HttpContext))
        {
            // If the API key is not valid, return an unauthorized result
            context.Result = new UnauthorizedResult();
        }
    }

    // Method to validate the provided API key
    private static bool IsApiKeyValid(HttpContext context)
    {
        // Retrieve the API key from the request headers
        string? apiKey = context.Request.Headers[ApiKeyHeaderName];

        // If API key is missing or empty, it's invalid
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            return false;
        }

        // Retrieve the expected API key from the configuration
        string actualApiKey = context.RequestServices
            .GetRequiredService<IConfiguration>()
            .GetValue<string>("Authentication:ApiKey")!;

        // Compare the provided API key with the expected one
        return apiKey == actualApiKey;
    }
}
