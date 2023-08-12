using ChatAI.Application.Common.Interfaces;
using ChatAI.Infrastructure.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace ChatAI.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly SmtpOptions _options;

    public EmailService(IOptions<SmtpOptions> options)
    {
        _options = options.Value;
    }

    public async Task SendAsync(MimeMessage message)
    {
        message.From.Add(new MailboxAddress(_options.SenderName, _options.SenderEmail));
        using var client = new SmtpClient();
        await client.ConnectAsync(_options.Host, _options.Port, SecureSocketOptions.Auto);
        await client.AuthenticateAsync(_options.Username, _options.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}
