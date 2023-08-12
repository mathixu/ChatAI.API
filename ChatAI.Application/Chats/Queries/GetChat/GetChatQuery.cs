using ChatAI.Application.Chats.DTOs;
using MediatR;

namespace ChatAI.Application.Chats.Queries.GetChat;

public class GetChatQuery : IRequest<ChatSessionResponse>
{
    public Guid Id { get; set; }

    public GetChatQuery(Guid id)
    {
        Id = id;
    }
}
