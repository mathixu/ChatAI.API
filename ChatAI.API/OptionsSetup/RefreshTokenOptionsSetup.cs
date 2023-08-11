using ChatAI.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace ChatAI.API.OptionsSetup;

public class RefreshTokenOptionsSetup : IConfigureOptions<RefreshTokenOptions>
{
    private const string SectionName = "RefreshToken";
    private readonly IConfiguration _configuration;

    public RefreshTokenOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(RefreshTokenOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}
