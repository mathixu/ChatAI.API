using AutoMapper;
using ChatAI.Application.Authentication.DTOs;
using ChatAI.Application.Common.Interfaces;
using ChatAI.Domain.Entities;
using ChatAI.Domain.Enums;

namespace ChatAI.Application.Common.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IMapper _mapper;
    private readonly IJwtProvider _jwtProvider;
    private readonly IRefreshTokenProvider _refreshTokenProvider;
    private readonly IEncryptionService _encryptionService;

    public AuthenticationService(
        IMapper mapper, IJwtProvider jwtProvider, IRefreshTokenProvider refreshTokenProvider, IEncryptionService encryptionService)
    {
        _mapper = mapper;
        _jwtProvider = jwtProvider;
        _refreshTokenProvider = refreshTokenProvider;
        _encryptionService = encryptionService;
    }

    public async Task<LoginResponse> GenerateUserAuthenticationTokens(User user)
    {
        var userResponse = _mapper.Map<LoginResponse>(user);

        var refreshToken = await _refreshTokenProvider.Generate(user);

        userResponse.AccessToken = _jwtProvider.Generate(user, JwtType.AccessToken);
        userResponse.RefreshToken = refreshToken.Token;

        await _refreshTokenProvider.RevokeAllExpired(user);

        if (userResponse.OpenAIToken is not null)
        {
            userResponse.OpenAIToken = _encryptionService.Decrypt(userResponse.OpenAIToken);
        }

        return userResponse;
    }
}
