using FluentValidation;

namespace ChatAI.Application.Account.Commands.DeleteMyAccount;

public class DeleteMyAccountCommandValidator : AbstractValidator<DeleteMyAccountCommand>
{
    public DeleteMyAccountCommandValidator()
    {
        RuleFor(v => v.Password).NotEmpty();
    }
}
