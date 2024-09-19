using SharedKernel.Primitives;

namespace SharedKernel.ValueObjects;

/// <summary>
/// Represents an API key as a value object.
/// </summary>
/// <remarks>
/// This value object encapsulates the API key value and the timestamp of its creation.
/// The use of a record type ensures that instances of this value object are immutable,
/// and equality is based on the values of the properties.
/// </remarks>
public sealed record ApiKey(
    string Value,
    DateTimeOffset CreatedDateTimeOffset) : ValueObject
{
    /// <summary>
    /// Factory method to create a new <see cref="ApiKey"/> instance.
    /// </summary>
    /// <param name="apiKey">The API key value as a string.</param>
    /// <returns>A new instance of <see cref="ApiKey"/> with the current timestamp.</returns>
    /// <remarks>
    /// The <see cref="DateTimeOffset.Now"/> is used to set the creation time,
    /// ensuring that the created timestamp is precise to the moment of instantiation.
    /// </remarks>
    public static ApiKey Create(string apiKey)
        => new(
            apiKey,
            DateTimeOffset.Now);
}
