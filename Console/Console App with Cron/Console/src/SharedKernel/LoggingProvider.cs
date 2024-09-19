using Serilog;

namespace SharedKernel;

public static class LoggingProvider
{
    private static string GenerateMessage(string type, string source, string operation, string message) => $"[{DateTime.Now}] - [{type.ToUpper()}] :: [{source}] - [{operation}] => [{message}]";

    public static void LogException(string source, string operation, Exception exception)
    {
        var error = GenerateMessage("ERROR", source, operation, $"Message: {exception.Message} - StackTrace: {exception.StackTrace}");

        Log.Error(error);
    }

    public static void LogError(string source, string operation, string message)
    {
        var error = GenerateMessage("ERROR", source, operation, $"Message: {message}");

        Log.Error(error);
    }

    public static void LogInformation(string source, string operation, string message)
    {
        var information = GenerateMessage("INFORMATION", source, operation, message);

        Log.Information(information);
    }
}
