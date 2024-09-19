using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using ErrorOr;
using API.Common.Http;

namespace API.Common.Errors;

// This class extends the ProblemDetailsFactory provided by ASP.NET Core
// to create custom ProblemDetails and ValidationProblemDetails objects
// with additional features and customization options.
public class CustomProblemDetailsFactory : ProblemDetailsFactory
{
    private readonly ApiBehaviorOptions _options;
    private readonly Action<ProblemDetailsContext>? _configure;

    // Constructor for the CustomProblemDetailsFactory class.
    // It takes in ApiBehaviorOptions and ProblemDetailsOptions as dependencies.
    public CustomProblemDetailsFactory(
        IOptions<ApiBehaviorOptions> options,
        IOptions<ProblemDetailsOptions>? problemDetailsOptions = null)
    {
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _configure = problemDetailsOptions?.Value?.CustomizeProblemDetails;
    }

    // Overrides the base class method to create a ProblemDetails object.
    public override ProblemDetails CreateProblemDetails(
        HttpContext httpContext,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        // Default status code is 500 if not provided.
        statusCode ??= 500;

        // Create a ProblemDetails object with the provided or default values.
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Type = type,
            Detail = detail,
            Instance = instance,
        };

        // Apply default settings and additional configurations.
        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }

    // Overrides the base class method to create a ValidationProblemDetails object.
    public override ValidationProblemDetails CreateValidationProblemDetails(
        HttpContext httpContext,
        ModelStateDictionary modelStateDictionary,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        // Ensure the modelStateDictionary is not null.
        ArgumentNullException.ThrowIfNull(modelStateDictionary);

        // Default status code is 400 if not provided.
        statusCode ??= 400;

        // Create a ValidationProblemDetails object with the provided or default values.
        var problemDetails = new ValidationProblemDetails(modelStateDictionary)
        {
            Status = statusCode,
            Type = type,
            Detail = detail,
            Instance = instance,
        };

        // If title is provided, set it (don't overwrite the default title with null).
        if (title != null)
        {
            problemDetails.Title = title;
        }

        // Apply default settings and additional configurations.
        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }

    // Applies default settings and additional configurations to a ProblemDetails object.
    private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int statusCode)
    {
        problemDetails.Status ??= statusCode;

        // Retrieve client error mapping from options and set title and type accordingly.
        if (_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
        {
            problemDetails.Title ??= clientErrorData.Title;
            problemDetails.Type ??= clientErrorData.Link;
        }

        // Add a traceId extension if available.
        var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
        if (traceId != null)
        {
            problemDetails.Extensions["traceId"] = traceId;
        }

        // Retrieve errors from HttpContext items and add errorCodes extension.
        var errors = httpContext?.Items[HttpContextItemKeys.Errors] as List<Error>;
        if (errors is not null)
        {
            problemDetails.Extensions.Add("errorCodes", errors.Select(e => e.Code));
        }

        // Invoke additional configuration if provided.
        _configure?.Invoke(new() { HttpContext = httpContext!, ProblemDetails = problemDetails });
    }
}
