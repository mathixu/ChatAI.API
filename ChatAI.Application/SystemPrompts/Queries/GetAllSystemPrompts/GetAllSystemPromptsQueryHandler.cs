using AutoMapper;
using ChatAI.Application.Common.Interfaces;
using ChatAI.Application.SystemPrompts.DTOs;
using ChatAI.Domain.Entities;
using MediatR;

namespace ChatAI.Application.SystemPrompts.Queries.GetAllSystemPrompts;

public class GetAllSystemPromptsQueryHandler : IRequestHandler<GetAllSystemPromptsQuery, List<SystemPromptResponse>>
{
    private readonly IBaseRepository<SystemPrompt> _repository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public GetAllSystemPromptsQueryHandler(IBaseRepository<SystemPrompt> repository, ICurrentUserService currentUserService, IMapper mapper)
    {
        _repository = repository;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<List<SystemPromptResponse>> Handle(GetAllSystemPromptsQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();

        var systemPrompts = await _repository.GetAll(sp => sp.UserId == currentUserId);

        var sytemPromptsWrapped = _mapper.Map<List<SystemPromptResponse>>(systemPrompts);

        return sytemPromptsWrapped;
    }
}
