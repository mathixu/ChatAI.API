using AutoMapper;
using ChatAI.Application.Chats.DTOs;
using ChatAI.Application.Chats.Queries.GetAllChats;
using ChatAI.Application.Common.Interfaces;
using ChatAI.Domain.Entities;
using MediatR;

namespace ChatAI.Application.Chats.Handlers;

public class GetAllChatsQueryHandler : IRequestHandler<GetAllChatsQuery, List<GetAllChatSessionsResponse>>
{
    private readonly IBaseRepository<ChatSession> _repository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public GetAllChatsQueryHandler(IBaseRepository<ChatSession> repository, ICurrentUserService currentUserService, IMapper mapper)
    {
        _repository = repository;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<List<GetAllChatSessionsResponse>> Handle(GetAllChatsQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.GetCurrentUserId();

        var chatSessions = await _repository.GetAll(cs => cs.UserId == currentUserId && cs.ForkedFromMessageId == null, q => q.OrderByDescending(e => e.CreatedAt));

        var chatSessionsWrapped = _mapper.Map<List<GetAllChatSessionsResponse>>(chatSessions);

        return chatSessionsWrapped;
    }
}
