using Domain.Common.Constants;
using FluentValidation;
using SharedKernel.ValueObjects;

namespace Application.Users.Commands.UpdateUser;

public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(Email.MaxEmailLength);

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(EntityConstants.MaxNameLength);
        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(EntityConstants.MaxNameLength);

        RuleFor(x => x.Roles)
            .Must(roles => roles is null || roles.Count is 0 || roles.All(role => int.TryParse(role.ToString(), out _)))
            .WithMessage("Roles must contain only numbers when provided.");
    }
}
