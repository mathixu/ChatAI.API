using AutoMapper;
using ChatAI.Application.Chats.DTOs;
using ChatAI.Application.Common.Exceptions;
using ChatAI.Application.Common.Interfaces;
using ChatAI.Domain.Entities;
using MediatR;

namespace ChatAI.Application.Chats.Commands.ForkMessage;

public class ForkMessageCommandHandler : IRequestHandler<ForkMessageCommand, ChatSessionResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly IChatSessionRepository _chatSessionRepository;

    public ForkMessageCommandHandler(ICurrentUserService currentUserService, IMapper mapper, IChatSessionRepository repository)
    {
        _currentUserService = currentUserService;
        _mapper = mapper;
        _chatSessionRepository = repository;
    }

    public async Task<ChatSessionResponse> Handle(ForkMessageCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();

        var chatSession = await _chatSessionRepository.Get(cs => cs.Id == request.ForkedFromChatSessionId && cs.UserId == currentUserId, new List<string> { "Messages" })
            ?? throw new NotFoundException(nameof(ChatSession), request.ForkedFromChatSessionId);

        if (!chatSession.Messages.Any(m => m.Id == request.ForkedFromMessageId && m.IsFromUser))
        {
            throw new NotFoundException(nameof(Message), request.ForkedFromMessageId);
        }

        var chatSessionByForkedMessage = _mapper.Map<ChatSession>(request);

        chatSessionByForkedMessage.UserId = currentUserId;

        await _chatSessionRepository.Insert(chatSessionByForkedMessage);

        var chatSessionWrapped = _mapper.Map<ChatSessionResponse>(chatSessionByForkedMessage);

        return chatSessionWrapped;
    }
}
