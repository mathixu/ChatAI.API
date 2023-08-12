namespace ChatAI.Domain.Entities;

public class ChatSession : BaseAuditableEntity
{
    public string? Title { get; set; }

    public List<Message> Messages { get; set; } = new();

    public Guid? ForkedFromMessageId { get; set; }
    public Message? ForkedFromMessage { get; set; } = default!;
    
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
}
