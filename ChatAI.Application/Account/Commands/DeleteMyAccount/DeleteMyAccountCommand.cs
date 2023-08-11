using MediatR;

namespace ChatAI.Application.Account.Commands.DeleteMyAccount;

public class DeleteMyAccountCommand : IRequest
{
    public string Password { get; set; } = default!;
}
