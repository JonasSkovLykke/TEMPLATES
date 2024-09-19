using SharedKernel.Primitives;

namespace SharedKernel.ValueObjects;

/// <summary>
/// Represents a phone number as a value object.
/// </summary>
/// <remarks>
/// The <see cref="PhoneNumber"/> value object is immutable and encapsulates the logic for handling phone numbers.
/// It ensures the phone number is stored and treated as a single entity in the domain model.
/// </remarks>
public sealed record PhoneNumber(string Value) : ValueObject
{
    /// <summary>
    /// The maximum length allowed for a phone number.
    /// </summary>
    /// <remarks>
    /// This constraint ensures that phone numbers exceeding this length are not valid within the system.
    /// </remarks>
    public const byte MaxPhoneNumberLength = 50;

    /// <summary>
    /// Creates a new <see cref="PhoneNumber"/> instance with the given phone number string.
    /// </summary>
    /// <param name="phoneNumber">The raw phone number as a string. Defaults to an empty string if not provided.</param>
    /// <returns>A new instance of the <see cref="PhoneNumber"/> value object.</returns>
    /// <remarks>
    /// This factory method allows creating a phone number and can be extended to include validation logic if needed.
    /// </remarks>
    public static PhoneNumber Create(string phoneNumber = "")
        => new(phoneNumber);
}
