﻿namespace FSH.WebApi.Domain.Geo;
public class GeoAdminUnit(string code, string name, string? fullName, string? navetiveName, string? fullNavetiveName, string? description, int grade, GeoAdminUnitType type, int order, bool isActive) : AuditableEntity, IAggregateRoot
{
    public int Order { get; private set; } = order;
    public string Code { get; private set; } = code;
    public string Name { get; private set; } = name;
    public string? Description { get; private set; } = description ?? string.Empty;
    public bool IsActive { get; set; } = isActive;

    public string? FullName { get; private set; } = fullName;
    public string? NativeName { get; private set; } = navetiveName;
    public string? FullNativeName { get; private set; } = fullNavetiveName;

    public int Grade { get; private set; } = grade;
    public GeoAdminUnitType Type { get; private set; } = type;

    public GeoAdminUnit()
        : this(string.Empty, string.Empty, null, null, null, null, 0, GeoAdminUnitType.Ward, 0, true)
    {
    }

    public GeoAdminUnit Update(
        string? code,
        string? name,
        string? fullName,
        string? nativeName,
        string? fullNativeName,
        string? description,
        int? grade,
        GeoAdminUnitType? type,
        int? order,
        bool? isActive)
    {
        if (order is not null && order.HasValue && Order != order) Order = order.Value;
        if (code is not null && Code?.Equals(code) is not true) Code = code;
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (isActive is not null && !IsActive.Equals(isActive)) IsActive = (bool)isActive;

        if (fullName is not null && FullName?.Equals(fullName) is not true) FullName = fullName;
        if (nativeName is not null && NativeName?.Equals(nativeName) is not true) NativeName = nativeName;
        if (fullNativeName is not null && FullNativeName?.Equals(fullNativeName) is not true) FullNativeName = fullNativeName;

        if (grade is not null && grade.HasValue && Grade != grade) Grade = grade.Value;
        if (type is not null && !Type.Equals(type)) Type = type.Value;

        return this;
    }
}
