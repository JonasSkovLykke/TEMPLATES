using Application.Common.Interfaces;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel.Interfaces;
using System.Diagnostics;

namespace Application.Behaviors;

/// <summary>
/// A behavior for logging request handling details, including execution time and errors.
/// </summary>
/// <typeparam name="TRequest">The type of request being handled.</typeparam>
/// <typeparam name="TResponse">The type of response returned by the request.</typeparam>
public class LoggingBehavior<TRequest, TResponse>(
    ILogger<LoggingBehavior<TRequest, TResponse>> _logger,
    IUser _user,
    IDateTimeProvider _dateTimeProvider) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly Stopwatch _timer = new();

    /// <summary>
    /// Handles the request by logging details and measuring execution time.
    /// </summary>
    /// <param name="request">The request being handled.</param>
    /// <param name="next">The delegate to invoke for the next behavior or request handler.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The response from the request handler.</returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Start measuring the time taken to handle the request.
        _timer.Start();

        // Invoke the next behavior or request handler to process the request.
        var result = await next();

        // If the result is an error, log the request failure details.
        if (result.IsError)
        {
            _logger.LogError(
                "Request failure {@RequestName}, UserId: {@UserId}, {@Errors}, {@DateTimeOffset}",
                typeof(TRequest).Name,
                _user.Id,
                result.Errors,
                _dateTimeProvider.Now);
        }

        // Stop measuring the time taken to handle the request.
        _timer.Stop();

        // Calculate the elapsed time in milliseconds.
        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        // If the request took more than 500 milliseconds, log it as a long-running request.
        if (elapsedMilliseconds > 500)
        {
            _logger.LogWarning(
                "Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}, UserId: {@UserId}",
                typeof(TRequest).Name,
                elapsedMilliseconds,
                request,
                _user.Id);
        }

        // Return the result of the request handling.
        return result;
    }
}
