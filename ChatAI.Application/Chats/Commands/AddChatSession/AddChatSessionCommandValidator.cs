using FluentValidation;

namespace ChatAI.Application.Chats.Commands.AddChatSession;

public class AddChatSessionCommandValidator : AbstractValidator<AddChatSessionCommand>
{
    public AddChatSessionCommandValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(100);
    }
}
