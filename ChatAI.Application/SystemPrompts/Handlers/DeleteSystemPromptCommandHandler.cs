﻿using ChatAI.Application.Exceptions;
using ChatAI.Application.Interfaces;
using ChatAI.Application.SystemPrompts.Commands.DeleteSystemPrompt;
using ChatAI.Domain.Entities;
using MediatR;

namespace ChatAI.Application.SystemPrompts.Handlers;

public class DeleteSystemPromptCommandHandler : IRequestHandler<DeleteSystemPromptCommand>
{
    private readonly IBaseRepository<SystemPrompt> _repository;
    private readonly ICurrentUserService _currentUserService;

    public DeleteSystemPromptCommandHandler(IBaseRepository<SystemPrompt> repository, ICurrentUserService currentUserService)
    {
        _repository = repository;
        _currentUserService = currentUserService;
    }

    public async Task Handle(DeleteSystemPromptCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.GetCurrentUserId() ?? throw new UnauthorizedAccessException();

        var systemPrompt = await _repository.Get(sp => sp.Id == request.Id && sp.UserId == currentUserId) ?? throw new NotFoundException(nameof(SystemPrompt), request.Id);

        await _repository.Delete(systemPrompt);
    }
}
