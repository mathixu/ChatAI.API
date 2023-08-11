namespace ChatAI.Domain.Entities;

public abstract class BaseToken : BaseEntity
{
    public string Token { get; set; } = default!;
    public DateTime ExpiresAt { get; set; }
}
