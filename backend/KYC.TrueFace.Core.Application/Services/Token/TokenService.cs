using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace KYC.TrueFace.Core.Application.Services.Token;

public class TokenService(
    IConfiguration configuration) : ITokenService
{
    public string GenerateToken(
        string username, 
        string role, 
        string? additionalClaims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SSO:Key"]!));
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
            issuer: configuration["SSO:Issuer"],
            audience: configuration["SSO:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}