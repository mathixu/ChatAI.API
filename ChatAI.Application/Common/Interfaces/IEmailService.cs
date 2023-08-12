using MimeKit;

namespace ChatAI.Application.Common.Interfaces;

public interface IEmailService
{
    Task SendAsync(MimeMessage message);
}
