using ChatAI.Application.Authentication.DTOs;
using MediatR;

namespace ChatAI.Application.Authentication.Commands.SignUp;

public class SignUpCommand : IRequest<LoginResponse>
{
    public string Email { get; set; } = default!;
    public string NickName { get; set; } = default!;
    public string Password { get; set; } = default!;
}
