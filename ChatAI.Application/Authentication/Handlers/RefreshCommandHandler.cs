using ChatAI.Domain.Entities;
using MediatR;
using ChatAI.Application.Authentication.DTOs;
using ChatAI.Application.Authentication.Commands.Refresh;
using ChatAI.Application.Common.Interfaces;

namespace ChatAI.Application.Authentication.Handlers;

public class RefreshCommandHandler : IRequestHandler<RefreshCommand, LoginResponse>
{
    private readonly IBaseRepository<RefreshToken> _repository;
    private readonly IAuthenticationService _authenticationService;
    private readonly IDateTime _dateTime;
    private readonly IRefreshTokenProvider _refreshTokenProvider;

    public RefreshCommandHandler(IBaseRepository<RefreshToken> repository, IAuthenticationService authenticationService, IDateTime dateTime, IRefreshTokenProvider refreshTokenProvider)
    {
        _repository = repository;
        _authenticationService = authenticationService;
        _dateTime = dateTime;
        _refreshTokenProvider = refreshTokenProvider;
    }

    public async Task<LoginResponse> Handle(RefreshCommand request, CancellationToken cancellationToken)
    {
        var token = await _repository.Get(x => x.Token == request.RefreshToken && x.ExpiresAt > _dateTime.Now,
            new List<string> { "User" }) ?? throw new UnauthorizedAccessException();
        
        await _refreshTokenProvider.Revoke(token);

        return await _authenticationService.GenerateUserAuthenticationTokens(token.User);
    }
}
