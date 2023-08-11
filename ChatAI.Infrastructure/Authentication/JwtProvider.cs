using ChatAI.Application.Interfaces;
using ChatAI.Domain.Entities;
using ChatAI.Domain.Enums;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChatAI.Infrastructure.Authentication;

public class JwtProvider : IJwtProvider
{
    private readonly IDateTime _dateTime;
    private readonly JwtOptions _jwtOptions;

    public JwtProvider(IDateTime dateTime, IOptions<JwtOptions> jwtOptions)
    {
        _dateTime = dateTime;
        _jwtOptions = jwtOptions.Value;
    }


    public string Generate(User user, JwtType type)
    {
        switch (type)
        {
            case JwtType.AccessToken:
                return GenerateAccessToken(user);
            case JwtType.PasswordResetToken:
                return GeneratePasswordResetToken(user);
            default:
                throw new ArgumentException($"Invalid value for {nameof(JwtType)}");
        }
    }

    private string GenerateAccessToken(User user)
    {
        var claims = GetAccessTokenClaims(user);

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            _dateTime.Now,
            _dateTime.Now.AddMinutes(_jwtOptions.ExpireInMinutes),
            signingCredentials
        );

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }

    private Claim[] GetAccessTokenClaims(User user)
    {
        return new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
    }

    private string GeneratePasswordResetToken(User user)
    {
        throw new NotImplementedException();
    }

}
