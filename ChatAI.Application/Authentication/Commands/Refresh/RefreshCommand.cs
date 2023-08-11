using ChatAI.Application.Authentication.DTOs;
using MediatR;

namespace ChatAI.Application.Authentication.Commands.Refresh;

public class RefreshCommand : IRequest<LoginResponse>
{
    public string RefreshToken { get; set; } = default!;
}
