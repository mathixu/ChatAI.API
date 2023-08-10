using MediatR;

namespace ChatAI.Application.Commands.Auth;

public class SignUpCommand : IRequest<LoginResponse>
{
    public string Email { get; set; } = default!;
    public string NickName { get; set; } = default!;
    public string Password { get; set; } = default!;
}
