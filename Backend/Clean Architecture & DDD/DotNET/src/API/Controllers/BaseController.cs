using API.Common.Http;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.Controllers;

/// <summary>
/// A base controller class that serves as the foundation for all API controllers in the application.
/// </summary>
[ApiController]
[Authorize]
//[ApiKey]
[Produces("application/json")]
public class BaseController : ControllerBase
{
    /// <summary>
    /// Gets the service resolver from the current HTTP context.
    /// </summary>
    protected IServiceProvider Resolver => HttpContext.RequestServices;

    /// <summary>
    /// Gets an instance of a service of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of service to retrieve.</typeparam>
    /// <returns>An instance of the specified service.</returns>
    protected T GetService<T>() => Resolver.GetService<T>()!;

    protected IMapper Mapper => GetService<IMapper>();

    /// <summary>
    /// Gets the MediatR sender for sending commands and queries.
    /// </summary>
    protected ISender Sender => GetService<ISender>();

    /// <summary>
    /// Handles generating an appropriate response for a list of errors.
    /// </summary>
    /// <param name="errors">The list of errors to handle.</param>
    /// <returns>An IActionResult representing the error response.</returns>
    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.Count == 0)
        {
            return Problem();
        }

        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }

        HttpContext.Items[HttpContextItemKeys.Errors] = errors;

        return Problem(errors[0]);
    }

    /// <summary>
    /// Generates an IActionResult representing an error.
    /// </summary>
    /// <param name="error">The error to generate the response for.</param>
    /// <returns>An IActionResult representing the error response.</returns>
    private IActionResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError,
        };

        return Problem(statusCode: statusCode, title: error.Description);
    }

    /// <summary>
    /// Generates an IActionResult representing a validation problem.
    /// </summary>
    /// <param name="errors">The list of validation errors.</param>
    /// <returns>An IActionResult representing the validation error response.</returns>
    private IActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();

        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(
                error.Code,
                error.Description);
        }

        return ValidationProblem(modelStateDictionary);
    }
}
