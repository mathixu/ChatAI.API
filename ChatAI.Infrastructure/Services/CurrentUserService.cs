using ChatAI.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ChatAI.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetCurrentUserEmail()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
    }

    public Guid? GetCurrentUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null || !Guid.TryParse(userId, out var userIdGuid))
        {
            return null;
        }

        return userIdGuid;
    }

    public string? GetCurrentUserToken()
    {
        var header = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"];

        return header?.ToString().Split(" ").Last();
    }
}
