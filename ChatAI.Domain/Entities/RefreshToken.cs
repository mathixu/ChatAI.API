namespace ChatAI.Domain.Entities;

public class RefreshToken : BaseToken
{
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
}
