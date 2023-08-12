using FluentValidation;

namespace ChatAI.Application.Chats.Commands.AddMessage;

public class AddMessageCommandValidator : AbstractValidator<AddMessageCommand>
{
    public AddMessageCommandValidator()
    {
        RuleFor(v => v.Content)
            .NotEmpty();

        RuleFor(v => v.IsFromUser)
            .Must(v => v == true || v == false);
    }
}
