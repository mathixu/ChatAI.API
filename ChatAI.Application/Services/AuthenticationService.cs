using AutoMapper;
using ChatAI.Application.Commands.Auth;
using ChatAI.Application.Interfaces;
using ChatAI.Domain.Entities;
using ChatAI.Domain.Enums;

namespace ChatAI.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IMapper _mapper;
    private readonly IJwtProvider _jwtProvider;

    public AuthenticationService(
        IMapper mapper, IJwtProvider jwtProvider)
    {
        _mapper = mapper;
        _jwtProvider = jwtProvider;
    }

    public async Task<LoginResponse> GenerateUserAuthenticationTokens(User user)
    {
        var userResponse = _mapper.Map<LoginResponse>(user);

        userResponse.AccessToken = _jwtProvider.Generate(user, JwtType.AccessToken);
        userResponse.RefreshToken = "refreshToken.Token";

        return userResponse;
    }
}
