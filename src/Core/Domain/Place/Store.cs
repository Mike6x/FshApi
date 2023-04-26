using FSH.WebApi.Domain.Geo;
using FSH.WebApi.Domain.Sales;

namespace FSH.WebApi.Domain.Place;
public class Store : AuditableEntity, IAggregateRoot
{
    public int Order { get; private set; }
    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }

    public string? FullName { get; private set; }
    public string? Latitude { get; private set; } // Vĩ độ
    public string? Longitude { get; private set; } // Kinh độ
    public string? Address { get; set; }
    public string? Email { get; private set; }
    public string? Phone { get; set; }
    public string? Fax { get; set; }
    public string? TaxCode { get; set; }

    public int Grade { get; private set; } // Cua hang cap 1,2,3
    public DefaultIdType RetailerId { get; set; }
    public virtual Retailer Retailer { get; set; } = default!;

    public DefaultIdType ProvinceId { get; set; }
    public virtual Province Province { get; set; } = default!;
    public DefaultIdType? DistrictId { get; set; }
    public virtual District? District { get; set; }
    public DefaultIdType? WardId { get; set; }
    public virtual Ward? Ward { get; set; }

    public virtual ICollection<Staff>? Staffs { get; private set; }
    public virtual ICollection<Stock>? Stocks { get; private set; }
    public virtual ICollection<Order>? Orders { get; private set; }

    public Store(
        int order,
        string code,
        string name,
        string? description,
        bool isActive,
        string? fullName,
        string? latitude,
        string? longitude,
        string? address,
        string? email,
        string? phone,
        string? fax,
        string? taxCode,
        int grade,
        DefaultIdType retailerId,
        DefaultIdType provinceId,
        DefaultIdType? districtId,
        DefaultIdType? wardId)
    {
        Order = order;
        Code = code;
        Name = name;
        Description = description;
        IsActive = isActive;

        FullName = fullName;
        Latitude = latitude;
        Longitude = longitude;
        Address = address;
        Email = email;
        Phone = phone;
        Fax = fax;
        TaxCode = taxCode;
        Grade = grade;

        RetailerId = retailerId;
        ProvinceId = provinceId;

        DistrictId = districtId == DefaultIdType.Empty ? null : districtId;
        WardId = (districtId == DefaultIdType.Empty || wardId == DefaultIdType.Empty) ? null : wardId;
    }

    public Store()
    : this(
        0,
        string.Empty,
        string.Empty,
        string.Empty,
        true,
        string.Empty,
        string.Empty,
        string.Empty,
        string.Empty,
        string.Empty,
        string.Empty,
        string.Empty,
        string.Empty,
        1,
        DefaultIdType.Empty,
        DefaultIdType.Empty,
        null,
        null)
    {
        Orders = new HashSet<Order>();
        Staffs = new HashSet<Staff>();
        Stocks = new HashSet<Stock>();
    }

    public Store Update(
        int? order,
        string? code,
        string? name,
        string? description,
        bool? isActive,
        string? fullName,
        string? latitude,
        string? longitude,
        string? address,
        string? email,
        string? phone,
        string? fax,
        string? taxCode,
        int? grade,
        DefaultIdType? retailerId,
        DefaultIdType? provinceId,
        DefaultIdType? districtId,
        DefaultIdType? wardId)
    {
        if (order is not null && order.HasValue && Order != order) Order = order.Value;
        if (code is not null && Code?.Equals(code) is not true) Code = code;
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (isActive is not null && !IsActive.Equals(isActive)) IsActive = (bool)isActive;

        if (fullName is not null && FullName?.Equals(fullName) is not true) FullName = fullName;

        if (latitude is not null && Latitude?.Equals(latitude) is not true) Latitude = latitude;
        if (longitude is not null && Longitude?.Equals(longitude) is not true) Longitude = longitude;
        if (address is not null && Address?.Equals(address) is not true) Address = address;
        if (email is not null && Email?.Equals(email) is not true) Email = email;
        if (phone is not null && Phone?.Equals(phone) is not true) Phone = phone;
        if (fax is not null && Fax?.Equals(fax) is not true) Fax = fax;
        if (taxCode is not null && TaxCode?.Equals(taxCode) is not true) TaxCode = taxCode;

        if (grade is not null && grade.HasValue && Grade != grade) Grade = grade.Value;

        if (retailerId.HasValue && retailerId.Value != DefaultIdType.Empty && !RetailerId.Equals(retailerId.Value)) RetailerId = retailerId.Value;
        if (provinceId.HasValue && provinceId.Value != DefaultIdType.Empty && !ProvinceId.Equals(provinceId.Value)) ProvinceId = provinceId.Value;

        if (districtId == DefaultIdType.Empty) DistrictId = null;
        else if (districtId.HasValue && !DistrictId.Equals(districtId.Value)) DistrictId = districtId.Value;

        if (wardId == DefaultIdType.Empty) WardId = null;
        else if (wardId.HasValue && !WardId.Equals(wardId.Value)) WardId = wardId.Value;

        return this;
    }
}