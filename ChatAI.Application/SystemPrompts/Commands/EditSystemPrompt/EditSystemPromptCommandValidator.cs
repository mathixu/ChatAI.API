using FluentValidation;

namespace ChatAI.Application.SystemPrompts.Commands.EditSystemPrompt;

public class EditSystemPromptCommandValidator : AbstractValidator<EditSystemPromptCommand>
{
	public EditSystemPromptCommandValidator()
	{
        RuleFor(v => v.Name).NotEmpty().MaximumLength(200);
        RuleFor(v => v.Prompt).NotEmpty().MaximumLength(1024);
    }
}
