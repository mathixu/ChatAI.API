using ChatAI.Application.Common.DTOs;

namespace ChatAI.Application.Chats.DTOs;

public class MessageResponse : BaseAuditableEntityResponse
{
    public string Content { get; set; } = default!;
    public bool IsFromUser { get; set; }
    public Guid ChatSessionId { get; set; }
}
