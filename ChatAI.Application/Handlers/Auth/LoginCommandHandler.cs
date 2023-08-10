using ChatAI.Application.Commands.Auth;
using ChatAI.Application.Interfaces;
using ChatAI.Domain.Entities;
using MediatR;

namespace ChatAI.Application.Handlers.Auth;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IBaseRepository<User> _userRepository;
    private readonly IHashService _hashService;

    public LoginCommandHandler(IBaseRepository<User> userRepository, IHashService hashService)
    {
        _userRepository = userRepository;
        _hashService = hashService;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Get(u => u.Email == request.Email);

        if (user is null)
        {
            throw new UnauthorizedAccessException();
        }

        if (!_hashService.Verify(request.Password, user.HashedPassword))
        {
            throw new UnauthorizedAccessException();
        }

        return new LoginResponse();
    }
}
