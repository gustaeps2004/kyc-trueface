using KYC.TrueFace.ApiPartner.Entities.Base;
using KYC.TrueFace.ApiPartner.Enums;

namespace KYC.TrueFace.ApiPartner.Entities;

public class UserAccessLog : EntityBase<Guid, int>
{
    public Guid CodeUserAccess { get; set; }
    public SituationUserAccessLog Situation { get; set; }
    public DateTime SituationDt { get; set; }
    public FlowSso Flow { get; set; }
    public required string Ip { get; set; }

    protected override void Validate() {  }
}