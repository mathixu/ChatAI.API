using AutoMapper;
using ChatAI.Application.Common.Interfaces;
using ChatAI.Application.Common.Exceptions;
using ChatAI.Application.SystemPrompts.Commands.ToggleFavoriteSystemPrompt;
using ChatAI.Application.SystemPrompts.DTOs;
using ChatAI.Domain.Entities;
using MediatR;

namespace ChatAI.Application.SystemPrompts.Handlers;

public class ToggleFavoriteSystemPromptCommandHandler : IRequestHandler<ToggleFavoriteSystemPromptCommand, SystemPromptResponse>
{
    private readonly IBaseRepository<SystemPrompt> _repository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public ToggleFavoriteSystemPromptCommandHandler(IBaseRepository<SystemPrompt> repository, ICurrentUserService currentUserService, IMapper mapper)
    {
        _repository = repository;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<SystemPromptResponse> Handle(ToggleFavoriteSystemPromptCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.GetCurrentUserId() ?? throw new UnauthorizedAccessException();

        var systemPrompt = await _repository.Get(sp => sp.Id == request.Id && sp.UserId == currentUserId) ?? throw new NotFoundException(nameof(SystemPrompt), request.Id);

        systemPrompt.IsFavorite = !systemPrompt.IsFavorite;

        await _repository.Update(systemPrompt);

        var systemPromptWrapper = _mapper.Map<SystemPromptResponse>(systemPrompt);

        return systemPromptWrapper;
    }
}
