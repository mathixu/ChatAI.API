using AutoMapper;
using ChatAI.Application.Chats.DTOs;
using ChatAI.Application.Common.Exceptions;
using ChatAI.Application.Common.Interfaces;
using ChatAI.Domain.Entities;
using MediatR;

namespace ChatAI.Application.Chats.Queries.GetChat;

public class GetChatQueryHandler : IRequestHandler<GetChatQuery, ChatSessionResponse>
{
    private readonly IChatSessionRepository _repository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public GetChatQueryHandler(IChatSessionRepository repository, ICurrentUserService currentUserService, IMapper mapper)
    {
        _repository = repository;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<ChatSessionResponse> Handle(GetChatQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();

        var chatSessions = await _repository.GetDeepAsync(cs => cs.Id == request.Id && cs.UserId == currentUserId)
            ?? throw new NotFoundException(nameof(ChatSession), request.Id);


        var chatSessionsWrapped = _mapper.Map<ChatSessionResponse>(chatSessions);

        return chatSessionsWrapped;
    }
}