using AutoMapper;
using ChatAI.Application.Common.Exceptions;
using ChatAI.Domain.Entities;
using MediatR;
using ChatAI.Application.Authentication.DTOs;
using MimeKit;
using ChatAI.Application.Common.Interfaces;

namespace ChatAI.Application.Authentication.Commands.SignUp;

public class SignUpCommandHandler : IRequestHandler<SignUpCommand, LoginResponse>
{
    private readonly IBaseRepository<User> _userRepository;
    private readonly IMapper _mapper;
    private readonly IHashService _hashService;
    private readonly IAuthenticationService _authenticationService;
    private readonly IEmailService _emailService;

    public SignUpCommandHandler(IBaseRepository<User> userRepository, IHashService hashService, IAuthenticationService authenticationService, IMapper mapper, IEmailService emailService)
    {
        _userRepository = userRepository;
        _hashService = hashService;
        _authenticationService = authenticationService;
        _mapper = mapper;
        _emailService = emailService;
    }

    public async Task<LoginResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Get(u => u.Email == request.Email);

        if (user is not null)
        {
            throw new AlreadyExistException(nameof(user), request.Email);
        }

        var newUser = _mapper.Map<User>(request);

        newUser.HashedPassword = _hashService.Hash(request.Password);

        await _userRepository.Insert(newUser);

        await SendWelcomeEmail(newUser);

        return await _authenticationService.GenerateUserAuthenticationTokens(newUser);
    }

    private async Task SendWelcomeEmail(User user)
    {
        var subject = "Welcome to ChatAI!";
        var body = $"Hello {user.NickName},\n\nThank you for signing up. We're glad to have you!\n\nBest regards,\nMathixu - ChatAI";

        var message = new MimeMessage();

        message.To.Add(new MailboxAddress(user.NickName, user.Email));
        message.Subject = subject;
        message.Body = new TextPart("plain")
        {
            Text = body
        };


        await _emailService.SendAsync(message);
    }
}
