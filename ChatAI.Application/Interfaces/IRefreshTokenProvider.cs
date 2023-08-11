using ChatAI.Domain.Entities;

namespace ChatAI.Application.Interfaces;

public interface IRefreshTokenProvider
{
    Task<RefreshToken> Generate(User user);
    Task Revoke(RefreshToken token);
    Task RevokeAll(User user);
    Task RevokeAllExpired(User user);
}
