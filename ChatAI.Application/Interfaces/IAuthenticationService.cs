using ChatAI.Application.Commands.Auth;
using ChatAI.Domain.Entities;

namespace ChatAI.Application.Interfaces;

public interface IAuthenticationService
{
    Task<LoginResponse> GenerateUserAuthenticationTokens(User user);
}
