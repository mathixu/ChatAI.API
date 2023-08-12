using AutoMapper;
using ChatAI.Application.Common.Interfaces;
using ChatAI.Application.SystemPrompts.Commands.AddSystemPrompt;
using ChatAI.Application.SystemPrompts.DTOs;
using ChatAI.Domain.Entities;
using MediatR;

namespace ChatAI.Application.SystemPrompts.Handlers;

public class AddSystemPromptCommandHandler : IRequestHandler<AddSystemPromptCommand, SystemPromptResponse>
{
    private readonly IBaseRepository<SystemPrompt> _repository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public AddSystemPromptCommandHandler(IBaseRepository<SystemPrompt> repository, ICurrentUserService currentUserService, IMapper mapper)
    {
        _repository = repository;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<SystemPromptResponse> Handle(AddSystemPromptCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.GetCurrentUserId() ?? throw new UnauthorizedAccessException();

        var systemPrompt = _mapper.Map<SystemPrompt>(request);
        systemPrompt.UserId = currentUserId;

        await _repository.Insert(systemPrompt);

        var systemPromptWrapper = _mapper.Map<SystemPromptResponse>(systemPrompt);

        return systemPromptWrapper;
    }
}
