using ChatAI.Domain.Entities;
using ChatAI.Application.Authentication.DTOs;

namespace ChatAI.Application.Interfaces;

public interface IAuthenticationService
{
    Task<LoginResponse> GenerateUserAuthenticationTokens(User user);
}
