using FSH.WebApi.Domain.Organization;
using FSH.WebApi.Domain.Sales;

namespace FSH.WebApi.Domain.People;

public class SubDepartment : AuditableEntity, IAggregateRoot
{
    public int Order { get; private set; }
    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = string.Empty;

    public bool IsActive { get; private set; }

    public DefaultIdType DepartmentId { get; private set; }
    public virtual Department Department { get; private set; } = default!;

    public SubDepartment(int order, string code, string name, string description, bool isActive, DefaultIdType departmentId)
    {
        Order = order;
        Code = code;
        Name = name;
        Description = description;
        IsActive = isActive;
        DepartmentId = departmentId;
    }

    public SubDepartment()
        : this(0, string.Empty, string.Empty, string.Empty, true, DefaultIdType.Empty)
    {
    }

    public SubDepartment Update(
        int? order,
        string? code,
        string? name,
        string? description,
        bool? isActive,
        DefaultIdType? departmentId)
    {
        if (order is not null && order.HasValue && Order != order) Order = order.Value;

        if (code is not null && Code?.Equals(code) is not true) Code = code;
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (isActive is not null && !IsActive.Equals(isActive)) IsActive = (bool)isActive;

        if (departmentId.HasValue && departmentId.Value != DefaultIdType.Empty && !DepartmentId.Equals(departmentId.Value)) DepartmentId = departmentId.Value;

        return this;
    }
}
