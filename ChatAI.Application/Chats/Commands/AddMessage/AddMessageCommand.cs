using ChatAI.Application.Chats.DTOs;
using MediatR;
using System.Text.Json.Serialization;

namespace ChatAI.Application.Chats.Commands.AddMessage;

public class AddMessageCommand : IRequest<MessageResponse>
{
    public string Content { get; set; } = default!;
    public bool IsFromUser { get; set; }

    [JsonIgnore]
    public Guid ChatSessionId { get; set; }

    public AddMessageCommand()
    {
    }

    public AddMessageCommand(Guid chatSessionId, AddMessageCommand addMessageCommand)
    {
        ChatSessionId = chatSessionId;
        Content = addMessageCommand.Content;
        IsFromUser = addMessageCommand.IsFromUser;
    }
}
