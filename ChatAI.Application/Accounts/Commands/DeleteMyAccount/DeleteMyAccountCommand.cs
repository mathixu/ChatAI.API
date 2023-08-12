using MediatR;

namespace ChatAI.Application.Accounts.Commands.DeleteMyAccount;

public class DeleteMyAccountCommand : IRequest
{
    public string Password { get; set; } = default!;
}
