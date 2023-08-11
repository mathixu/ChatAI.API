using ChatAI.Application.Interfaces;
using ChatAI.Application.Account.Commands.AddOpenAIToken;
using MediatR;
using ChatAI.Domain.Entities;

namespace ChatAI.Application.Account.Handlers;

public class AddOpenAITokenCommandHandler : IRequestHandler<AddOpenAITokenCommand>
{
    private readonly IBaseRepository<User> _userRepository;
    private readonly ICurrentUserService _currentUserService;

    public AddOpenAITokenCommandHandler(IBaseRepository<User> userRepository, ICurrentUserService currentUserService)
    {
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }

    public async Task Handle(AddOpenAITokenCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetCurrentUserId() ?? throw new UnauthorizedAccessException();
        
        var user = await _userRepository.Get(userId) ?? throw new UnauthorizedAccessException();

        user.OpenAIToken = request.OpenAIToken;

        await _userRepository.Update(user);
    }
}