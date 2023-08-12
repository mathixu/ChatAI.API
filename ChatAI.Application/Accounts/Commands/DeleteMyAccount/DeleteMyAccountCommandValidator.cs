using FluentValidation;

namespace ChatAI.Application.Accounts.Commands.DeleteMyAccount;

public class DeleteMyAccountCommandValidator : AbstractValidator<DeleteMyAccountCommand>
{
    public DeleteMyAccountCommandValidator()
    {
        RuleFor(v => v.Password).NotEmpty();
    }
}
