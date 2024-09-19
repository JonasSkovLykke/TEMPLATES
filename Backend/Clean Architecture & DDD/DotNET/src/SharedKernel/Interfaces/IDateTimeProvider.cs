namespace SharedKernel.Interfaces;

/// <summary>
/// Represents an interface for providing date and time information.
/// </summary>
public interface IDateTimeProvider
{
    /// <summary>
    /// Gets the current UTC date and time with an offset.
    /// </summary>
    DateTimeOffset Now { get; }
}
