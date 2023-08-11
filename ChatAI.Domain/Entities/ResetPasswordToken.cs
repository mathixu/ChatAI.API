namespace ChatAI.Domain.Entities;

public class ResetPasswordToken : BaseToken
{
    public bool IsUsed { get; set; } = default!;
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
}
