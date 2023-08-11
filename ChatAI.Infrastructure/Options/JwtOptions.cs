namespace ChatAI.Infrastructure.Options;

public class JwtOptions
{
    public string Issuer { get; init; } = default!;
    public string Audience { get; init; } = default!;
    public string SecretKey { get; init; } = default!;
    public int AccessTokenExpireInMinutes { get; init; }
    public int ResetPasswordTokenExpireInMinutes { get; init; }
}
