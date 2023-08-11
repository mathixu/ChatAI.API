using ChatAI.Application.Common.DTOs;

namespace ChatAI.Application.Authentication.DTOs;

public class LoginResponse : BaseAuditableEntityResponse
{
    public string Email { get; set; } = default!;
    public string NickName { get; set; } = default!;
    public string? OpenAIToken { get; set; } = null;
    public string AccessToken { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}
