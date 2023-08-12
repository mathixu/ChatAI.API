using ChatAI.Application.Interfaces;
using ChatAI.Domain.Entities;
using MediatR;
using MimeKit;
using ChatAI.Application.Authentication.Commands.ResetPassword;

namespace ChatAI.Application.Authentication.Handlers;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
{
    private readonly IBaseRepository<User> _userRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IBaseRepository<ResetPasswordToken> _resetPasswordTokenRepository;
    private readonly IDateTime _dateTime;
    private readonly IEmailService _emailService;
    private readonly IHashService _hashService;

    public ResetPasswordCommandHandler(IBaseRepository<User> userRepository, IDateTime dateTime, IEmailService emailService, IBaseRepository<ResetPasswordToken> resetPasswordTokenRepository, ICurrentUserService currentUserService, IHashService hashService)
    {
        _userRepository = userRepository;
        _dateTime = dateTime;
        _emailService = emailService;
        _resetPasswordTokenRepository = resetPasswordTokenRepository;
        _currentUserService = currentUserService;
        _hashService = hashService;
    }

    public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var token = _currentUserService.GetCurrentUserToken();

        var resetPasswordToken = await _resetPasswordTokenRepository.Get(x => x.Token == token && x.ExpiresAt > _dateTime.Now && !x.IsUsed, new List<string> { "User" }) 
            ?? throw new UnauthorizedAccessException();

        var user = resetPasswordToken.User;

        user.HashedPassword = _hashService.Hash(request.Password);
        
        resetPasswordToken.IsUsed = true;

        await _userRepository.Update(user);
        await _resetPasswordTokenRepository.Update(resetPasswordToken);

        await SendResetPasswordConfirmation(user);
    }

    private async Task SendResetPasswordConfirmation(User user)
    {
        var subject = "Your password has been successfully reset";
        var body = $"Dear {user.NickName},\n\nWe would like to inform you that your password has been successfully reset on our platform https://chatai.mathixu.dev/. You can now log in to your account using your new password.\n\nFor security reasons, we remind you that it is not recommended to share your password with others.\n\nIf you did not request to reset your password, we invite you to contact us immediately at contact@mathixu.dev or through our contact form on our website. Our teams are available to assist you.\n\nThank you for your trust and we wish you a pleasant experience on our platform.\n\nSincerely,\n\nMathixu - ChatAI";

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
