namespace FSH.WebApi.Domain.Settings;

public class EntityCode(
    int order,
    string code,
    string name,
    string? description,
    string separator,
    int value,
    CodeType? type,
    bool? isActive)
    : AuditableEntity, IAggregateRoot
{
    public int Order { get; private set; } = order;

    /// <summary>
    /// Code = NameOf(Entity).
    /// </summary>
    public string Code { get; private set; } = code;

    /// <summary>
    /// Name = Prefix of EntityCode: example "Cus" in  Custommer Code string Cus_20241230_001.
    /// </summary>
    public string Name { get; private set; } = name;
    public string Description { get; private set; } = description ?? string.Empty;

    public string Seperator { get; private set; } = separator;
    public int? Value { get; private set; } = value;
    public CodeType Type { get; private set; } = type ?? CodeType.MasterData;

    public bool IsActive { get; private set; } = isActive ?? true;

    public EntityCode()
        : this(0, string.Empty, string.Empty, string.Empty, string.Empty, 0, CodeType.MasterData, true)
    {
    }

    public EntityCode Update(
    int? order,
    string? code,
    string? name,
    string? description,
    string? seperator,
    int? value,
    CodeType? type,
    bool? isActive)
    {
        if (order is not null && order.HasValue && Order != order) Order = order.Value;
        if (code is not null && Code?.Equals(code) is not true) Code = code;
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;

        if (seperator is not null && Seperator?.Equals(seperator) is not true) Seperator = seperator;
        if (value is not null && value.HasValue && Value != value) Value = value.Value;
        if (type is not null && !Type.Equals(CodeType.All) && !Type.Equals(type)) Type = type.Value;
        if (isActive is not null && !IsActive.Equals(isActive)) IsActive = (bool)isActive;

        return this;
    }

    public string AutoCode => GenerateCode();

    private string GenerateCode()
    {
        return Type switch
        {
            CodeType.MasterData => Name + Seperator + DateTime.UtcNow.ToString("yyyyMMdd_HHmm"),
            CodeType.Transaction => Name + Seperator + DateTime.UtcNow.ToString("yyyyMMdd_HHmmss"),
            CodeType.FastTransaction => Name + Seperator + DateTime.UtcNow.ToString("yyyyMMdd_HHmmss"),

            _ => DefaultIdType.NewGuid().ToString(),
        };
    }
}