using ChatAI.Application.Chats.DTOs;
using MediatR;

namespace ChatAI.Application.Chats.Queries.GetAllChats;

public class GetAllChatsQuery : IRequest<List<GetAllChatSessionsResponse>>
{
}
