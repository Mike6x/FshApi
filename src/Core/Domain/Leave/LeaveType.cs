namespace FSH.WebApi.Domain.Leave;

public class LeaveType : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public int DefaultDays { get; set; }

    public LeaveType(string name, string? description, int defaultDays)
    {
        Name = name;
        Description = description;
        DefaultDays = defaultDays;
    }

    public LeaveType()
    : this(string.Empty, string.Empty, 0)
    {
    }

    public LeaveType Update(string? name, string? description, int defaultDays)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (defaultDays > 0 && DefaultDays != defaultDays) DefaultDays = defaultDays;
        return this;
    }
}