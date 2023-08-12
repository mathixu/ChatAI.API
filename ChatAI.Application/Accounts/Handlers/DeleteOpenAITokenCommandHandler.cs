using ChatAI.Application.Accounts.Commands.DeleteOpenAIToken;
using ChatAI.Application.Common.Interfaces;
using ChatAI.Domain.Entities;
using MediatR;

namespace ChatAI.Application.Accounts.Handlers;

public class DeleteOpenAITokenCommandHandler : IRequestHandler<DeleteOpenAITokenCommand>
{
    private readonly IBaseRepository<User> _repository;
    private readonly ICurrentUserService _currentUserService;

    public DeleteOpenAITokenCommandHandler(IBaseRepository<User> repository, ICurrentUserService currentUserService)
    {
        _repository = repository;
        _currentUserService = currentUserService;
    }

    public async Task Handle(DeleteOpenAITokenCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.GetCurrentUserId() ?? throw new UnauthorizedAccessException();

        var user = await _repository.Get(currentUserId) ?? throw new UnauthorizedAccessException();
        
        user.OpenAIToken = null;

        await _repository.Update(user);
    }
}
