namespace KYC.TrueFace.Core.Application.Services.Token;

public interface ITokenService
{
    string GenerateToken(
        string username, 
        string role, 
        string? additionalClaims
    );
}