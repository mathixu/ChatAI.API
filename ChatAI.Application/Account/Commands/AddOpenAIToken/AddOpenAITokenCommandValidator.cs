using FluentValidation;

namespace ChatAI.Application.Account.Commands.AddOpenAIToken;

public class AddOpenAITokenCommandValidator : AbstractValidator<AddOpenAITokenCommand>
{
    public AddOpenAITokenCommandValidator()
    {
        RuleFor(v => v.OpenAIToken)
            .NotEmpty()
            .MaximumLength(200);
    }
}
