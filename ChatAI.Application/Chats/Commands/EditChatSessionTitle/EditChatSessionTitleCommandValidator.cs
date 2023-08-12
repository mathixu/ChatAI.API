using FluentValidation;

namespace ChatAI.Application.Chats.Commands.EditChatSessionTitle;

public class EditChatSessionTitleCommandValidator : AbstractValidator<EditChatSessionTitleCommand>
{
    public EditChatSessionTitleCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(100)
            .NotEmpty();
    }
}
