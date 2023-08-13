using AutoMapper;
using ChatAI.Application.Common.Interfaces;
using ChatAI.Application.Common.Exceptions;
using ChatAI.Application.SystemPrompts.DTOs;
using ChatAI.Domain.Entities;
using MediatR;

namespace ChatAI.Application.SystemPrompts.Queries.GetSystemPrompt;

public class GetSystemPromptQueryHandler : IRequestHandler<GetSystemPromptQuery, SystemPromptResponse>
{
    private readonly IBaseRepository<SystemPrompt> _repository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public GetSystemPromptQueryHandler(IBaseRepository<SystemPrompt> repository, ICurrentUserService currentUserService, IMapper mapper)
    {
        _repository = repository;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<SystemPromptResponse> Handle(GetSystemPromptQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();

        var systemPrompt = await _repository.Get(sp => sp.Id == request.Id && sp.UserId == currentUserId) ?? throw new NotFoundException(nameof(SystemPrompt), request.Id);

        var sytemPromptWrapped = _mapper.Map<SystemPromptResponse>(systemPrompt);

        return sytemPromptWrapped;
    }
}
