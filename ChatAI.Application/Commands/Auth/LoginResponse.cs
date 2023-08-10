using ChatAI.Application.Common.DTOs;

namespace ChatAI.Application.Commands.Auth;

public class LoginResponse : BaseAuditableEntityResponse
{
    public string Email { get; set; } = default!;
    public string NickName { get; set; } = default!;
    public string AccessToken { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}
