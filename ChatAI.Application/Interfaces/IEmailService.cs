using MimeKit;

namespace ChatAI.Application.Interfaces;

public interface IEmailService
{
    Task SendAsync(MimeMessage message);
}
