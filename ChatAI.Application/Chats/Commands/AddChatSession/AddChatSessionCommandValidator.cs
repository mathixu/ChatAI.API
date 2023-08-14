using ChatAI.Domain.Enums;
using FluentValidation;

namespace ChatAI.Application.Chats.Commands.AddChatSession;

public class AddChatSessionCommandValidator : AbstractValidator<AddChatSessionCommand>
{
    public AddChatSessionCommandValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(100);

        RuleFor(x => x.SystemInstruction)
            .NotEmpty();

        RuleFor(x => x.Model)
            .NotEmpty()
            .Must(IsValidEnumValue).WithMessage("Enter a valid model");
    }

    private bool IsValidEnumValue(string model)
    {
        return Enum.TryParse(typeof(GPTModel), model, true, out _);
    }
}
