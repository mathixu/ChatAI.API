using ChatAI.Application.Authentication.DTOs;
using MediatR;

namespace ChatAI.Application.Authentication.Commands.Login;

public class LoginCommand : IRequest<LoginResponse>
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}
