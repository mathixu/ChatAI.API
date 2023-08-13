using AutoMapper;
using ChatAI.Application.Chats.DTOs;
using ChatAI.Application.Common.Interfaces;
using ChatAI.Domain.Entities;
using MediatR;

namespace ChatAI.Application.Chats.Queries.GetAllChats;

public class GetAllChatsQueryHandler : IRequestHandler<GetAllChatsQuery, List<GetAllChatSessionsResponse>>
{
    private readonly IChatSessionRepository _repository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public GetAllChatsQueryHandler(IChatSessionRepository repository, ICurrentUserService currentUserService, IMapper mapper)
    {
        _repository = repository;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<List<GetAllChatSessionsResponse>> Handle(GetAllChatsQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();

        var chatSessions = await _repository.GetAll(cs => cs.UserId == currentUserId && cs.ForkedFromMessageId == null, q => q.OrderByDescending(e => e.CreatedAt));

        var chatSessionsWrapped = _mapper.Map<List<GetAllChatSessionsResponse>>(chatSessions);

        return chatSessionsWrapped;
    }
}
