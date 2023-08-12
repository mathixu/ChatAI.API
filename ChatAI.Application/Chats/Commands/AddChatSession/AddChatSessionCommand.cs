using ChatAI.Application.Chats.DTOs;
using MediatR;

namespace ChatAI.Application.Chats.Commands.AddChatSession;

public class AddChatSessionCommand : IRequest<ChatSessionCreatedResponse>
{
    public string? Title { get; set; }
}
