using AutoMapper;
using ChatAI.Application.Chats.DTOs;
using ChatAI.Application.Chats.Queries.GetChat;
using ChatAI.Application.Common.Exceptions;
using ChatAI.Application.Common.Interfaces;
using ChatAI.Domain.Entities;
using MediatR;

namespace ChatAI.Application.Chats.Handlers;

public class GetChatQueryHandler : IRequestHandler<GetChatQuery, ChatSessionResponse>
{
    private readonly IBaseRepository<ChatSession> _repository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public GetChatQueryHandler(IBaseRepository<ChatSession> repository, ICurrentUserService currentUserService, IMapper mapper)
    {
        _repository = repository;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<ChatSessionResponse> Handle(GetChatQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.GetCurrentUserId();

        var chatSessions = await _repository.Get(cs => cs.Id == request.Id && cs.UserId == currentUserId, new List<string> { "Messages" }) 
            ?? throw new NotFoundException(nameof(ChatSession), request.Id);
        
        
        var chatSessionsWrapped = _mapper.Map<ChatSessionResponse>(chatSessions);

        return chatSessionsWrapped;
    }
}
