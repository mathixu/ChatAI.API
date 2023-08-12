using FluentValidation;

namespace ChatAI.Application.SystemPrompts.Commands.AddSystemPrompt;

public class AddSystemPromptCommandValidator : AbstractValidator<AddSystemPromptCommand>
{
	public AddSystemPromptCommandValidator()
	{
        RuleFor(v => v.Name).NotEmpty().MaximumLength(200);
        RuleFor(v => v.Prompt).NotEmpty().MaximumLength(1024);
    }
}
