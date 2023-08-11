using MediatR;

namespace ChatAI.Application.Authentication.Commands.RequestPasswordReset;

public class RequestPasswordResetCommand : IRequest
{
    public string Email { get; set; } = default!;
}
