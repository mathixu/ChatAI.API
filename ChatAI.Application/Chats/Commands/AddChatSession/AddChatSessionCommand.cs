using ChatAI.Application.Chats.DTOs;
using MediatR;

namespace ChatAI.Application.Chats.Commands.AddChatSession;

public class AddChatSessionCommand : IRequest<ChatSessionCreatedResponse>
{
}
