using ChatAI.Domain.Entities;
using MediatR;
using ChatAI.Domain.Enums;
using MimeKit;
using Microsoft.Extensions.Configuration;
using ChatAI.Application.Common.Interfaces;

namespace ChatAI.Application.Authentication.Commands.RequestPasswordReset;

public class RequestPasswordResetCommandHandler : IRequestHandler<RequestPasswordResetCommand>
{
    private readonly IBaseRepository<User> _userRepository;
    private readonly IBaseRepository<ResetPasswordToken> _resetPasswordTokenRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IDateTime _dateTime;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;

    public RequestPasswordResetCommandHandler(IBaseRepository<User> userRepository, IJwtProvider jwtProvider, IDateTime dateTime, IEmailService emailService, IConfiguration configuration, IBaseRepository<ResetPasswordToken> resetPasswordTokenRepository)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        _dateTime = dateTime;
        _emailService = emailService;
        _configuration = configuration;
        _resetPasswordTokenRepository = resetPasswordTokenRepository;
    }

    public async Task Handle(RequestPasswordResetCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Get(x => x.Email == request.Email, new List<string> { "ResetPasswordTokens" });

        if (user is null)
        {
            return;
        }

        if (user.ResetPasswordTokens.Any(x => x.ExpiresAt > _dateTime.Now && !x.IsUsed))
        {
            return;
        }

        var token = _jwtProvider.Generate(user, JwtType.PasswordResetToken);

        var expireInMinutes = int.Parse(_configuration["Jwt:ResetPasswordTokenExpireInMinutes"] ?? throw new InvalidOperationException());

        var resetPasswordToken = new ResetPasswordToken
        {
            IsUsed = false,
            ExpiresAt = _dateTime.Now.AddMinutes(expireInMinutes),
            Token = token,
            UserId = user.Id,
        };

        await _resetPasswordTokenRepository.Insert(resetPasswordToken);

        await SendResetPasswordEmail(user, token);
    }

    private async Task SendResetPasswordEmail(User user, string token)
    {
        var link = $"https://chatai.mathixu.dev/forgot-password/{token}";
        var expireInMinutes = _configuration["Jwt:ResetPasswordTokenExpireInMinutes"];

        var subject = "Reset your password on ChatAI";
        var body = $"Dear {user.NickName},\n\nWe have received a request to reset your password for your account on our platform https://chatai.mathixu.dev/. To proceed with the reset, please click on the link below:\n\n{link}\n\nThis link will expire in {expireInMinutes} minutes from the receipt of this email. If you did not request to reset your password, it is possible that someone entered your email address by mistake. In this case, please ignore this email and no action will be required on your part.\n\nIf you encounter any issues resetting your password or if you need additional assistance, please do not hesitate to contact us at contact@mathixu.dev or through our contact form on our website. Our teams are available to assist you.\n\nThank you for your trust and we wish you a pleasant experience on our platform.\n\nSincerely,\n\nMathixu - ChatAI";

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
