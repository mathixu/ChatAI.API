using MediatR;

namespace ChatAI.Application.Authentication.Commands.ResetPassword;

public class ResetPasswordCommand : IRequest
{
    public string Password { get; set; } = default!;
}
