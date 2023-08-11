using ChatAI.Application.Interfaces;
using ChatAI.Domain.Entities;
using ChatAI.Domain.Enums;
using ChatAI.Infrastructure.Options;
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
        var claims = GetClaims(user, type);

        var expireInMinutes = type switch
        {
            JwtType.AccessToken => _jwtOptions.AccessTokenExpireInMinutes,
            JwtType.PasswordResetToken => _jwtOptions.ResetPasswordTokenExpireInMinutes,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

        return WriteToken(claims, expireInMinutes);
    }

    private string WriteToken(Claim[] claims, int expireInMinutes)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Audience,
                claims,
                _dateTime.Now,
                _dateTime.Now.AddMinutes(expireInMinutes
            ),
            signingCredentials
        );

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }

    private Claim[] GetClaims(User user, JwtType type)
    {
        return new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("purpose", type.ToString())
        };
    }
}
