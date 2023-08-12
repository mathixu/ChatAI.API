using ChatAI.Application.Common.DTOs;

namespace ChatAI.Application.Chats.DTOs;

public class GetAllChatSessionsResponse : BaseAuditableEntityResponse
{
    public string? Title { get; set; }
    public Guid? ForkedFromMessageId { get; set; }
}
