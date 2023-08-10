using MediatR;

namespace ChatAI.Application.Commands.Auth;

public class LoginCommand : IRequest<LoginResponse>
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}
