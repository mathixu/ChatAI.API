using FluentValidation;

namespace ChatAI.Application.Authentication.Commands.Refresh;

public class RefreshCommandValidator : AbstractValidator<RefreshCommand>
{
    public RefreshCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithMessage("Refresh token is required");

        RuleFor(x => x.RefreshToken)
            .Must(x => Guid.TryParse(x, out _))
            .WithMessage("Refresh token is not valid");
    }
}
