namespace ChatAI.Application.Common.Interfaces;

public interface ICurrentUserService
{
    string? UserEmail { get; }
    Guid? UserId { get; }
    string? UserToken { get; }
}