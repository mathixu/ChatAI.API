using AutoMapper;
using ChatAI.Application.Chats.DTOs;
using ChatAI.Application.Common.Interfaces;
using ChatAI.Domain.Entities;
using MediatR;

namespace ChatAI.Application.Chats.Commands.AddChatSession;

public class AddChatSessionCommandHandler : IRequestHandler<AddChatSessionCommand, ChatSessionCreatedResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IChatSessionRepository _repository;
    private readonly IMapper _mapper;

    public AddChatSessionCommandHandler(ICurrentUserService currentUserService, IChatSessionRepository repository, IMapper mapper)
    {
        _currentUserService = currentUserService;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ChatSessionCreatedResponse> Handle(AddChatSessionCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();

        var chatSession = _mapper.Map<ChatSession>(request);

        chatSession.UserId = currentUserId;

        await _repository.Insert(chatSession);

        var chatSessionWrapped = _mapper.Map<ChatSessionCreatedResponse>(chatSession);

        return chatSessionWrapped;
    }
}
