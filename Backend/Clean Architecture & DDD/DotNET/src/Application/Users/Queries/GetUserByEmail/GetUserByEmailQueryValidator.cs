using FluentValidation;

namespace Application.Users.Queries.GetUserByEmail;

public sealed class GetUserByEmailQueryValidator : AbstractValidator<GetUserByEmailQuery>
{
    public GetUserByEmailQueryValidator()
    {
        RuleFor(x => x.Email)
            .NotNull();
    }
}
