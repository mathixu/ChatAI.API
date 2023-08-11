namespace ChatAI.Application.Interfaces;

public interface ICurrentUserService
{
    string? GetCurrentUserEmail();
    Guid? GetCurrentUserId();
    string? GetCurrentUserToken();
}
