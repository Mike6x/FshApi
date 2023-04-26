// namespace FSH.WebApi.Domain.Common.Contracts;

// public abstract class AuditableBaseEntity : AuditableBaseEntity<DefaultIdType>
// {

// }

// public abstract class AuditableBaseEntity<T> : BaseEntity<T>, IAuditableEntity, ISoftDelete
// {
//    public int Order { get; private set; }
//    public string Code { get; private set; } = default!;
//    public string Name { get; private set; } = default!;
//    public string? Description { get; private set; }
//    public bool IsActive { get; set; }
//    public Guid CreatedBy { get; set; }
//    public DateTime CreatedOn { get; set; }
//    public Guid LastModifiedBy { get; set; }
//    public DateTime? LastModifiedOn { get; set; }
//    public DateTime? DeletedOn { get; set; }
//    public Guid? DeletedBy { get; set; }
//    protected AuditableBaseEntity()
//    {
//        CreatedOn = DateTime.UtcNow;
//        LastModifiedOn = DateTime.UtcNow;
//    }
// }
