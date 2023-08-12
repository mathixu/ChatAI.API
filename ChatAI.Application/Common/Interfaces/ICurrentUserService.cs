namespace ChatAI.Application.Common.Interfaces;

public interface ICurrentUserService
{
    string? GetCurrentUserEmail();
    Guid? GetCurrentUserId();
    string? GetCurrentUserToken();
}
