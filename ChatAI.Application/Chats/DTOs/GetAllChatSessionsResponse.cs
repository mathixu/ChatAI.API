using ChatAI.Application.Common.DTOs;

namespace ChatAI.Application.Chats.DTOs;

public class GetAllChatSessionsResponse : BaseAuditableEntityResponse
{
    public string? Title { get; set; }
    public string SystemInstruction { get; set; } = default!;
    public string Model { get; set; } = default!;

    public Guid? ForkedFromMessageId { get; set; }
}
