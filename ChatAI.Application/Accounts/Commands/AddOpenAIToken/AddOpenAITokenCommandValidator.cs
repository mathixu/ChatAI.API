using FluentValidation;

namespace ChatAI.Application.Accounts.Commands.AddOpenAIToken;

public class AddOpenAITokenCommandValidator : AbstractValidator<AddOpenAITokenCommand>
{
    public AddOpenAITokenCommandValidator()
    {
        RuleFor(v => v.OpenAIToken)
            .NotEmpty()
            .MaximumLength(200);
    }
}
