using FluentValidation;

namespace ChatAI.Application.Chats.Commands.ForkMessage;

public class ForkMessageCommandValidator : AbstractValidator<ForkMessageCommand>
{
    public ForkMessageCommandValidator()
    {
        RuleFor(v => v.Content)
            .NotEmpty();

        RuleFor(v => v.IsFromUser)
            .Must(v => v == true || v == false);
    }
}
