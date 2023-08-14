using ChatAI.Application.Common.DTOs;

namespace ChatAI.Application.Chats.DTOs;

public class ChatSessionCreatedResponse : BaseAuditableEntityResponse
{
    public string? Title { get; set; }
    public string SystemInstruction { get; set; } = default!;
    public string Model { get; set; } = default!;
}
