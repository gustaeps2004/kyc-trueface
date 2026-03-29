namespace KYC.TrueFace.ApiPartner.Entities.Base;

public abstract class EntityBase<TPrimaryKey, TId>
{
    public TId? ID { get; set; }
    public TPrimaryKey? Code { get; set; }

    protected abstract void Validate();
}
