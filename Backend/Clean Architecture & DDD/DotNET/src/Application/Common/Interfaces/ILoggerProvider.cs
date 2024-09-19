namespace Application.Common.Interfaces;

/// <summary>
/// Provider interface for logging errors.
/// </summary>
public interface ILoggerProvider
{
    /// <summary>
    /// Logs an error along with information about the source and operation.
    /// </summary>
    /// <param name="ex">The exception to be logged.</param>
    /// <param name="source">The source or class where the error occurred.</param>
    /// <param name="operation">The specific operation or method where the error occurred.</param>
    void LogError(Exception ex, string source, string operation);

    /// <summary>
    /// Logs information about an operation.
    /// </summary>
    /// <param name="message">The message to be logged.</param>
    /// <param name="source">The source or class where the operation occurred.</param>
    /// <param name="operation">The specific operation or method.</param>
    void LogInformation(string message, string source, string operation);
}
