using AutoMapper;
using ChatAI.Application.Chats.Commands.AddMessage;
using ChatAI.Application.Chats.DTOs;
using ChatAI.Application.Common.Exceptions;
using ChatAI.Application.Common.Interfaces;
using ChatAI.Domain.Entities;
using MediatR;

namespace ChatAI.Application.Chats.Handlers;

public class AddMessageCommandHandler : IRequestHandler<AddMessageCommand, MessageResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IBaseRepository<ChatSession> _chatSessionRepository;
    private readonly IBaseRepository<Message> _messageRepository;
    private readonly IMapper _mapper;

    public AddMessageCommandHandler(ICurrentUserService currentUserService, IMapper mapper, IBaseRepository<ChatSession> chatSessionRepository, IBaseRepository<Message> messageRepository)
    {
        _currentUserService = currentUserService;
        _mapper = mapper;
        _chatSessionRepository = chatSessionRepository;
        _messageRepository = messageRepository;
    }

    public async Task<MessageResponse> Handle(AddMessageCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.GetCurrentUserId() ?? throw new UnauthorizedAccessException();

        var chatSession = await _chatSessionRepository.Get(cs => cs.Id == request.ChatSessionId && cs.UserId == currentUserId)
            ?? throw new NotFoundException(nameof(ChatSession), request.ChatSessionId);

        
        var message = _mapper.Map<Message>(request);

        await _messageRepository.Insert(message);

        var messageWrapped = _mapper.Map<MessageResponse>(message);

        return messageWrapped;
    }
}
