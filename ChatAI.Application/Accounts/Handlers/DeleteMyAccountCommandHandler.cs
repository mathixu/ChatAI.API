using MediatR;
using ChatAI.Application.Accounts.Commands.DeleteMyAccount;
using ChatAI.Domain.Entities;
using ChatAI.Application.Common.Interfaces;

namespace ChatAI.Application.Accounts.Handlers;

public class DeleteMyAccountCommandHandler : IRequestHandler<DeleteMyAccountCommand>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IBaseRepository<User> _userRepository;
    private readonly IHashService _hashService;

    public DeleteMyAccountCommandHandler(ICurrentUserService currentUserService, IBaseRepository<User> userRepository, IHashService hashService)
    {
        _currentUserService = currentUserService;
        _userRepository = userRepository;
        _hashService = hashService;
    }

    public async Task Handle(DeleteMyAccountCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetCurrentUserId() ?? throw new UnauthorizedAccessException();

        var user = await _userRepository.Get(userId) ?? throw new UnauthorizedAccessException();

        if (!_hashService.Verify(request.Password, user.HashedPassword))
        {
            throw new UnauthorizedAccessException();
        }

        await _userRepository.Delete(user);
    }
}