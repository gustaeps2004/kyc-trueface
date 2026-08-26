using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using KYC.TrueFace.Core.Domain.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace KYC.TrueFace.Core.Application.Services.Token;

public class TokenService(
    IOptions<SsoOptions> ssoOptions) : ITokenService
{
    public string GenerateToken(
        string username,
        string role,
        string? additionalClaims)
    {
        var sso = ssoOptions.Value;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(sso.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, username),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new("role", role.ToUpper())
        };

        if (!string.IsNullOrWhiteSpace(additionalClaims))
        {
            var additionalClaimsDes = JsonSerializer.Deserialize<Dictionary<string, string>>(additionalClaims);

            foreach (var claim in additionalClaimsDes!)
                claims.Add(new Claim(claim.Key, claim.Value));
        }

        var token = new JwtSecurityToken(
            issuer: sso.Issuer,
            audience: sso.Audience,
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}