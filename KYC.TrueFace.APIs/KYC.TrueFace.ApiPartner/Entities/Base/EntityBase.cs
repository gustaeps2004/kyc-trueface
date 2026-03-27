namespace KYC.TrueFace.ApiPartner.Entities.Base;

public abstract class EntityBase<TPrimaryKey, TId>
{
    public required TId ID { get; set; }
    public required TPrimaryKey Code { get; set; }

    protected abstract void Validate();
}
