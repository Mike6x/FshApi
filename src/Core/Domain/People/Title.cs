namespace FSH.WebApi.Domain.People;

public class Title : AuditableEntity, IAggregateRoot
{
    public int Order { get; private set; }
    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = string.Empty;

    /// <summary>
    /// value of title.
    /// </summary>
    ///
    public int Grade { get; private set; }
    public bool IsActive { get; private set; }

    public Title(int order, string code, string name, string? description, int grade, bool? isActive)
    {
        Order = order;
        Code = code;
        Name = name;
        Description = description ?? string.Empty;
        Grade = grade;
        IsActive = isActive ?? true;
    }

    public Title()
        : this(0, string.Empty, string.Empty, string.Empty, 0, true)
    {
    }

    public Title Update(
    int? order,
    string? code,
    string? name,
    string? description,
    int? grade,
    bool? isActive)
    {
        if (order is not null && order.HasValue && Order != order) Order = order.Value;
        if (code is not null && Code?.Equals(code) is not true) Code = code;
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;

        if (grade is not null && grade.HasValue && Grade != grade) Grade = grade.Value;
        if (isActive is not null && !IsActive.Equals(isActive)) IsActive = (bool)isActive;

        return this;
    }
}
