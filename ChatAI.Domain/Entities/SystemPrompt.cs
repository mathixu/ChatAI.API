namespace ChatAI.Domain.Entities;

public class SystemPrompt : BaseAuditableEntity
{
    public string Name { get; set; } = default!;
    public string Prompt { get; set; } = default!;
    public bool IsFavorite { get; set; } = false;

    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
}
