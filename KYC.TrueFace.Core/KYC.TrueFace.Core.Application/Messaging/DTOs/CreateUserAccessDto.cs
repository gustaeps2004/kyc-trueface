namespace KYC.TrueFace.Core.Application.Messaging.DTOs;

public class CreateUserAccessDto(
    string username, 
    string password, 
    List<string> role, 
    Dictionary<string, string> claims)
{
    public string Username { get; set; } = username;
    public string Password { get; set; } = password;
    public List<string> Role { get; set; } = role;
    public Dictionary<string, string> Claims { get; set; } = claims;
}