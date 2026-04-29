using KYC.TrueFace.Core.Domain.Entities.Base;
using KYC.TrueFace.Core.Domain.Enums;

namespace KYC.TrueFace.Core.Domain.Entities;

public class UserAccessLog : EntityBase
{
    public Guid CodeUserAccess { get; set; }
    public DateTime SituationDt { get; set; }
    public FlowIdentity Flow { get; set; }
    public required string Ip { get; set; }
}