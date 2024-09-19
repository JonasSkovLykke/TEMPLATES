using SharedKernel.Interfaces;

namespace SharedKernel.Services;

/// <summary>
/// A service that provides the current UTC date and time.
/// </summary>
public sealed class DateTimeProvider : IDateTimeProvider
{
    /// <summary>
    /// Gets the current UTC date and time with an offset.
    /// </summary>
    public DateTimeOffset Now => DateTimeOffset.Now;
}
