using ChatAI.Domain.Enums;
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

        RuleFor(x => x.Model)
        .NotEmpty()
        .Must(IsValidEnumValue).WithMessage("Enter a valid model");
    }

    private bool IsValidEnumValue(string model)
    {
        return Enum.TryParse(typeof(GPTModel), model, true, out _);
    }
}
