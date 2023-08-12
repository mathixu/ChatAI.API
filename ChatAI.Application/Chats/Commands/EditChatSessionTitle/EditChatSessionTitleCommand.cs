using ChatAI.Application.Chats.DTOs;
using MediatR;
using System.Text.Json.Serialization;

namespace ChatAI.Application.Chats.Commands.EditChatSessionTitle;

public class EditChatSessionTitleCommand : IRequest<ChatSessionCreatedResponse>
{
    public string Title { get; set; } = default!;

    [JsonIgnore]
    public Guid ChatSessionId { get; set; }

    public EditChatSessionTitleCommand()
    {
    }

    public EditChatSessionTitleCommand(Guid chatSessionId, EditChatSessionTitleCommand editChatSessionTitleCommand)
    {
        ChatSessionId = chatSessionId;
        Title = editChatSessionTitleCommand.Title;
    }
}
