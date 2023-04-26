using FSH.WebApi.Domain.Place;

namespace FSH.WebApi.Domain.Sales;
public class Staff : AuditableEntity, IAggregateRoot
{
    public string Code { get; private set; }
    public string FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string? Phone { get; private set; }
    public string? Email { get; private set; }
    public bool Active { get; private set; }

    public DefaultIdType? ManagerId { get; private set; }
    public virtual Staff Manager { get; private set; } = default!;
    public DefaultIdType? StoreId { get; private set; }
    public virtual Store Store { get; private set; } = default!;

    public virtual ICollection<Staff> InverseManager { get; private set; } = default!;
    public virtual ICollection<Order> Orders { get; private set; } = default!;

    public Staff(string code, string firstName, string? lastName, string phone, string? email, bool active, DefaultIdType? managerId, DefaultIdType? storeId)
    {
        Code = code;
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
        Active = active;
        StoreId = storeId;
        ManagerId = managerId;
    }

    public Staff()
        : this(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true, null, null)
        {
        }

    public Staff Update(string? code, string? firstName, string? lastName, string? phone, string? email, bool? active, DefaultIdType? managerId, DefaultIdType? storeId)
    {
        if (code is not null && Code?.Equals(code) is not true) Code = code;
        if (firstName is not null && FirstName?.Equals(firstName) is not true) FirstName = firstName;
        if (lastName is not null && LastName?.Equals(lastName) is not true) LastName = lastName;
        if (phone is not null && Phone?.Equals(phone) is not true) Phone = phone;
        if (email is not null && Email?.Equals(email) is not true) Email = email;
        if (active is not null && !Active.Equals(active)) Active = (bool)active;
        if (managerId.HasValue && managerId.Value != DefaultIdType.Empty && !ManagerId.Equals(managerId.Value)) ManagerId = managerId.Value;
        if (storeId.HasValue && storeId.Value != DefaultIdType.Empty && !StoreId.Equals(storeId.Value)) StoreId = storeId.Value;

        return this;
    }
}
