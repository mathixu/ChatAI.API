using AutoMapper;
using ChatAI.Application.Chats.Commands.AddChatSession;
using ChatAI.Application.Chats.DTOs;
using ChatAI.Application.Common.Interfaces;
using ChatAI.Domain.Entities;
using MediatR;

namespace ChatAI.Application.Chats.Handlers;

public class AddChatSessionCommandHandler : IRequestHandler<AddChatSessionCommand, ChatSessionCreatedResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IBaseRepository<ChatSession> _repository;
    private readonly IMapper _mapper;

    public AddChatSessionCommandHandler(ICurrentUserService currentUserService, IBaseRepository<ChatSession> repository, IMapper mapper)
    {
        _currentUserService = currentUserService;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ChatSessionCreatedResponse> Handle(AddChatSessionCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.GetCurrentUserId() ?? throw new UnauthorizedAccessException();
        
        var chatSession = new ChatSession
        {
            UserId = currentUserId,
        };

        await _repository.Insert(chatSession);

        var chatSessionWrapped = _mapper.Map<ChatSessionCreatedResponse>(chatSession);

        return chatSessionWrapped;
    }
}
