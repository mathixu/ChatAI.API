using ChatAI.Domain.Entities;
using ChatAI.Application.Authentication.DTOs;

namespace ChatAI.Application.Common.Interfaces;

public interface IAuthenticationService
{
    Task<LoginResponse> GenerateUserAuthenticationTokens(User user);
}
