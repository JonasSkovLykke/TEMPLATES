using Microsoft.Extensions.Logging;
using SharedKernel.Interfaces;

namespace Infrastructure.Services;

/// <summary>
/// Provider implementation for logging errors using Microsoft.Extensions.Logging.
/// Initializes a new instance of the <see cref="LoggerProvider"/> class.
/// </summary>
/// <param name="logger">The logger instance for logging errors.</param>
/// <param name="dateTimeProvider">The provider for obtaining date and time information.</param>
public sealed class LoggerProvider(
    ILogger<LoggerProvider> _logger,
    IDateTimeProvider _dateTimeProvider) : Application.Common.Interfaces.ILoggerProvider
{
    /// <summary>
    /// Logs an error along with information about the source and operation.
    /// </summary>
    /// <param name="ex">The exception to be logged.</param>
    /// <param name="source">The source or class where the error occurred.</param>
    /// <param name="operation">The specific operation or method where the error occurred.</param>
    public void LogError(Exception ex, string source, string operation)
    {
        _logger.LogError(
            "An exception occurred while performing an operation in the {@Source} " +
            "Operation: {@Operation}. " +
            "Exception Message: {@Message}. " +
            "Stack Trace: {@StackTrace}. " +
            "Occurred at: {@DateTimeOffset}.",
            source,
            operation,
            ex.Message,
            ex.StackTrace,
            _dateTimeProvider.Now);
    }

    /// <summary>
    /// Logs information about the operation.
    /// </summary>
    /// <param name="message">The message to be logged.</param>
    /// <param name="source">The source or class where the operation occurred.</param>
    /// <param name="operation">The specific operation or method.</param>
    public void LogInformation(string message, string source, string operation)
    {
        _logger.LogInformation(
            "Information: {@Message} Source: {@Source} Operation: {@Operation} Occurred at: {@DateTimeOffset}",
            message,
            source,
            operation,
            _dateTimeProvider.Now);
    }
}
