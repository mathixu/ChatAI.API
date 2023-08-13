using ChatAI.Application.Chats.DTOs;
using MediatR;
using System.Text.Json.Serialization;

namespace ChatAI.Application.Chats.Commands.ForkMessage;

public class ForkMessageCommand : IRequest<ChatSessionResponse>
{
    public string Content { get; set; } = default!;
    public bool IsFromUser { get; set; }

    [JsonIgnore]
    public Guid ForkedFromMessageId { get; set; }

    [JsonIgnore]
    public Guid ForkedFromChatSessionId { get; set; }

    public ForkMessageCommand()
    {
    }

    public ForkMessageCommand(Guid sessionId, Guid messageId, ForkMessageCommand forkMessageCommand)
    {
        ForkedFromChatSessionId = sessionId;
        ForkedFromMessageId = messageId;
        Content = forkMessageCommand.Content;
        IsFromUser = forkMessageCommand.IsFromUser;
    }
}
