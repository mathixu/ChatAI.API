using ChatAI.Application.Common.Interfaces;
using ChatAI.Domain.Entities;
using ChatAI.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace ChatAI.Infrastructure.Authentication;

class RefreshTokenProvider : IRefreshTokenProvider
{
    private readonly IBaseRepository<RefreshToken> _repository;
    private readonly IDateTime _dateTime;
    private readonly RefreshTokenOptions _options;

    public RefreshTokenProvider(IBaseRepository<RefreshToken> repository, 
        IDateTime dateTime, 
        IOptions<RefreshTokenOptions> options
        )
    {
        _repository = repository;
        _dateTime = dateTime;
        _options = options.Value;
    }

    public async Task<RefreshToken> Generate(User user)
    {
        var refreshToken = new RefreshToken
        {
            UserId = user.Id,
            Token = Guid.NewGuid().ToString(),
            ExpiresAt = _dateTime.Now.AddDays(_options.ExpireInDays)
        };

        await _repository.Insert(refreshToken);

        return refreshToken;
    }

    public async Task Revoke(RefreshToken token)
    {
        await _repository.Delete(token.Id);
    }

    public async Task RevokeAll(User user)
    {
        await _repository.Delete(t => t.UserId == user.Id);
    }

    public async Task RevokeAllExpired(User user)
    {
        await _repository.Delete(t => t.UserId == user.Id && t.ExpiresAt < _dateTime.Now);
    }
}
