using KYC.TrueFace.Core.Domain.Entities.Base;
using KYC.TrueFace.Core.Domain.Enums;

namespace KYC.TrueFace.Core.Domain.Entities;

public class UserAccess : EntityBase
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public Situation Situation { get; set; }
    public string Role { get; set; } = null!;
    public string Claim { get; set; } = null!;

    public UserAccess() {  }
    public UserAccess(
        string username, 
        string password, 
        string role, 
        string claim)
    {
        Code = Guid.NewGuid();
        InclusionDt = DateTime.UtcNow;
        Username = username;
        Password = password;
        Situation = Situation.Enabled;
        Role = role;
        Claim = claim;
    }

    public void UpdatePassword(string password)
        => Password = password;
}