namespace FSH.WebApi.Domain.Property;

public class AssetStatus : AuditableEntity, IAggregateRoot
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }

    public AssetStatusType Type { get; private set; }

    public AssetStatus(string code, string name, string? description, AssetStatusType type)
    {
        Code = code;
        Name = name;
        Description = description;
        Type = type;
    }

    public AssetStatus()
    : this(string.Empty, string.Empty, string.Empty, AssetStatusType.Quality)
    {
    }

    public AssetStatus Update(string? code, string? name, string? description, AssetStatusType? type)
    {
        if (code is not null && Code?.Equals(code) is not true) Code = code;
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;

        if (type is not null && !Type.Equals(type)) Type = type.Value;

        return this;
    }
}
