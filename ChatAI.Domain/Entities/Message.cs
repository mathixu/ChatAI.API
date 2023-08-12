namespace ChatAI.Domain.Entities;

public class Message : BaseAuditableEntity
{
    public string Content { get; set; } = default!;
    public bool IsFromUser { get; set; }

    public ChatSession ChatSession { get; set; } = default!;
    public Guid ChatSessionId { get; set; }
}
