namespace ChatAI.Domain.Entities;

public class User : BaseAuditableEntity
{
    public string Email { get; set; } = default!;
    public string NickName { get; set; } = default!;
    public string HashedPassword { get; set; } = default!;
    public string? OpenAIToken { get; set; } = null;
    public List<RefreshToken> RefreshTokens { get; set; } = new();
    public List<ResetPasswordToken> ResetPasswordTokens { get; set; } = new();
}
