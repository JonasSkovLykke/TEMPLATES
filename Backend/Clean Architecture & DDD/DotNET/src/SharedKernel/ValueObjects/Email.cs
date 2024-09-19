using ErrorOr;
using SharedKernel.Primitives;

namespace SharedKernel.ValueObjects;

/// <summary>
/// Represents an email address as a value object. 
/// This class is immutable and provides validation for the email address format.
/// </summary>
/// <remarks>
/// The <see cref="Email"/> value object encapsulates the logic for validating and ensuring a valid email address.
/// It also defines constraints such as the maximum allowed length for the email.
/// </remarks>
public sealed record Email(string Value) : ValueObject
{
    /// <summary>
    /// The maximum length allowed for an email address.
    /// </summary>
    public const byte MaxEmailLength = 50;

    /// <summary>
    /// Creates a new <see cref="Email"/> instance after validating the email string.
    /// </summary>
    /// <param name="email">The raw email address to validate and create.</param>
    /// <returns>An <see cref="ErrorOr{T}"/> containing the valid <see cref="Email"/> object if successful, 
    /// or an error if validation fails.</returns>
    /// <remarks>
    /// The method performs basic validation: it checks if the email is empty or improperly formatted. 
    /// It returns predefined errors when validation fails.
    /// </remarks>
    public static ErrorOr<Email> Create(string email)
    {
        // Check if the email is empty or contains only white spaces
        if (string.IsNullOrWhiteSpace(email))
        {
            return EmailErrors.Empty;
        }

        // Validate that the email contains exactly one '@' symbol
        if (email.Split('@').Length is not 2)
        {
            return EmailErrors.InvalidFormat;
        }

        // Return the valid Email value object
        return new Email(email);
    }
}

/// <summary>
/// Defines the validation errors associated with the <see cref="Email"/> value object.
/// </summary>
/// <remarks>
/// This static class provides centralized definitions of errors, ensuring that validation errors are consistent and reusable.
/// </remarks>
public static class EmailErrors
{
    /// <summary>
    /// Error returned when the email address is empty.
    /// </summary>
    public static readonly Error Empty = Error.Validation(
        code: "Email.EmailEmpty",
        description: "Email is empty.");

    /// <summary>
    /// Error returned when the email address format is invalid.
    /// </summary>
    public static readonly Error InvalidFormat = Error.Validation(
        code: "Email.InvalidFormat",
        description: "Email format is invalid.");
}
