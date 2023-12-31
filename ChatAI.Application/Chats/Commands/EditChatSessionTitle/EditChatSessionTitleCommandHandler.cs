﻿using AutoMapper;
using ChatAI.Application.Chats.DTOs;
using ChatAI.Application.Common.Exceptions;
using ChatAI.Application.Common.Interfaces;
using ChatAI.Domain.Entities;
using MediatR;

namespace ChatAI.Application.Chats.Commands.EditChatSessionTitle;

public class EditChatSessionTitleCommandHandler : IRequestHandler<EditChatSessionTitleCommand, ChatSessionCreatedResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IChatSessionRepository _repository;
    private readonly IMapper _mapper;

    public EditChatSessionTitleCommandHandler(ICurrentUserService currentUserService, IMapper mapper, IChatSessionRepository repository)
    {
        _currentUserService = currentUserService;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ChatSessionCreatedResponse> Handle(EditChatSessionTitleCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();

        var chatSession = await _repository.Get(cs => cs.Id == request.ChatSessionId && cs.UserId == currentUserId && cs.ForkedFromMessageId == null)
            ?? throw new NotFoundException(nameof(ChatSession), request.ChatSessionId);

        chatSession.Title = request.Title;

        await _repository.Update(chatSession);

        var chatSessionWrapped = _mapper.Map<ChatSessionCreatedResponse>(chatSession);

        return chatSessionWrapped;
    }
}
