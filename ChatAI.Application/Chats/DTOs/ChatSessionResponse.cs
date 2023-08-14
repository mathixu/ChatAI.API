using ChatAI.Application.Common.DTOs;

namespace ChatAI.Application.Chats.DTOs;

public class ChatSessionResponse : BaseAuditableEntityResponse
{
    public string? Title { get; set; }
    public string? SystemInstruction { get; set; }
    public string Model { get; set; } = default!;
    
    public List<MessageResponse> Messages { get; set; } = new();

    public Guid? ForkedFromMessageId { get; set; }

    public List<ChatSessionResponse> ForkedChatSessions { get; set; } = new();
    public Guid? ForkedFromChatSessionId { get; set; }
}
