using AutoMapper;
using ChatAI.Application.Authentication.DTOs;
using ChatAI.Application.Interfaces;
using ChatAI.Domain.Entities;
using ChatAI.Domain.Enums;

namespace ChatAI.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IMapper _mapper;
    private readonly IJwtProvider _jwtProvider;
    private readonly IRefreshTokenProvider _refreshTokenProvider;

    public AuthenticationService(
        IMapper mapper, IJwtProvider jwtProvider, IRefreshTokenProvider refreshTokenProvider)
    {
        _mapper = mapper;
        _jwtProvider = jwtProvider;
        _refreshTokenProvider = refreshTokenProvider;
    }

    public async Task<LoginResponse> GenerateUserAuthenticationTokens(User user)
    {
        var userResponse = _mapper.Map<LoginResponse>(user);

        var refreshToken = await _refreshTokenProvider.Generate(user);

        userResponse.AccessToken = _jwtProvider.Generate(user, JwtType.AccessToken);
        userResponse.RefreshToken = refreshToken.Token;

        await _refreshTokenProvider.RevokeAllExpired(user);

        return userResponse;
    }
}
