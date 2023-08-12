using ChatAI.Application.Authentication.Commands.LogoutAllDevices;
using ChatAI.Application.Interfaces;
using ChatAI.Domain.Entities;
using MediatR;

namespace ChatAI.Application.Authentication.Handlers;

public class LogoutAllDevicesCommandHandler : IRequestHandler<LogoutAllDevicesCommand>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IBaseRepository<User> _userRepository;
    private readonly IRefreshTokenProvider _refreshTokenProvider;

    public LogoutAllDevicesCommandHandler(ICurrentUserService currentUserService, IRefreshTokenProvider refreshTokenProvider, IBaseRepository<User> userRepository)
    {
        _currentUserService = currentUserService;
        _refreshTokenProvider = refreshTokenProvider;
        _userRepository = userRepository;
    }

    public async Task Handle(LogoutAllDevicesCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.GetCurrentUserId() ?? throw new UnauthorizedAccessException();

        var user = await _userRepository.Get(currentUserId) ?? throw new UnauthorizedAccessException();

        await _refreshTokenProvider.RevokeAll(user);
    }
}
