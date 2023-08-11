namespace ChatAI.Infrastructure.Options;

public class SmtpOptions
{
    public string Host { get; set; } = default!;
    public int Port { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string SenderName { get; set; } = default!;
    public string SenderEmail { get; set; } = default!;
}
