using KYC.TrueFace.ApiPartner.Entities.Base;
using KYC.TrueFace.ApiPartner.Enums;

namespace KYC.TrueFace.ApiPartner.Entities;

public class UserAccess : EntityBase<Guid, int>
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public string Scope { get; set; }
    public DateTime InclusionDt { get; set; }
    public SituationAccess Situation { get; set; }

    public UserAccess(string username, byte[] password)
    {
        Code = Guid.NewGuid();
        Username = username;
        Password = string.Join("", password);
        InclusionDt = DateTime.Now;
        Situation = SituationAccess.Active;
        Role = "teste";
        Scope = "teste 2";
    }

    protected override void Validate() {  }
}