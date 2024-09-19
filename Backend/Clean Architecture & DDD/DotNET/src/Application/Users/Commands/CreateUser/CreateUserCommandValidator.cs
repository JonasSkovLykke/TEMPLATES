using Domain.Common.Constants;
using FluentValidation;
using SharedKernel.ValueObjects;
using System.Text.RegularExpressions;

namespace Application.Users.Commands.CreateUser;

public sealed partial class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(Email.MaxEmailLength);

        RuleFor(x => x.Password)
                .NotEmpty()
                .Must(p => PasswordRegex().IsMatch(p))
                .WithMessage("Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character.");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(EntityConstants.MaxNameLength);
        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(EntityConstants.MaxNameLength);
    }

    // Regular expression pattern for a strong password
    [GeneratedRegex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,}$")]
    private static partial Regex PasswordRegex();
}