using FSH.WebApi.Domain.Geo;
using FSH.WebApi.Domain.Price;

namespace FSH.WebApi.Domain.Place;

/// <summary>
/// Type : Distributor/Supermarket.
/// </summary>
public class Retailer : AuditableEntity, IAggregateRoot
{
    public int Order { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }

    public string? FullName { get; private set; }
    public string? Latitude { get; private set; } // Vĩ độ
    public string? Longitude { get; private set; } // Kinh độ
    public string? Address { get; private set; }
    public string? Email { get; private set; }
    public string? Phone { get; private set; }
    public string? Fax { get; private set; }
    public string? TaxCode { get; private set; }
    public int Grade { get; private set; } // Dai ly cap 1,2,3

    public string SellGroup { get; private set; }

    public DefaultIdType ChannelId { get; private set; }
    public virtual Channel Channel { get; private set; } = default!;
    public virtual ICollection<Store> Stores { get; private set; } = default!;

    public DefaultIdType ProvinceId { get; private set; }
    public virtual Province Province { get; private set; } = default!;
    public DefaultIdType? DistrictId { get; private set; }
    public virtual District? District { get; private set; }
    public DefaultIdType? WardId { get; private set; }
    public virtual Ward? Ward { get; private set; }

    public DefaultIdType? PriceGroupId { get; private set; } // Mã nhóm giá
    public virtual PriceGroup? PriceGroup { get; private set; }

    // public virtual ICollection<Contact> Contacts { get; private set; }
    public Retailer(
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
        string sellGroup,
        DefaultIdType channelId,
        DefaultIdType provinceId,
        DefaultIdType? districtId,
        DefaultIdType? wardId,
        DefaultIdType? priceGroupId)
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
        SellGroup = sellGroup;

        ChannelId = channelId;

        ProvinceId = provinceId;
        DistrictId = districtId == DefaultIdType.Empty ? null : districtId;
        WardId = (districtId == DefaultIdType.Empty || wardId == DefaultIdType.Empty) ? null : wardId;

        PriceGroupId = priceGroupId == DefaultIdType.Empty ? null : priceGroupId;
    }

    public Retailer()
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
        "D2C",
        DefaultIdType.Empty,
        DefaultIdType.Empty,
        null,
        null,
        null)
    {
    }

    public Retailer Update(
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
        string? sellGroup,
        DefaultIdType? channelId,
        DefaultIdType? provinceId,
        DefaultIdType? districtId,
        DefaultIdType? wardId,
        DefaultIdType? priceGroupId)
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

        if (sellGroup is not null && SellGroup?.Equals(sellGroup) is not true) SellGroup = sellGroup;

        if (channelId.HasValue && channelId.Value != DefaultIdType.Empty && !ChannelId.Equals(channelId.Value)) ChannelId = channelId.Value;
        if (provinceId is not null && provinceId.Value != DefaultIdType.Empty && !ProvinceId.Equals(provinceId.Value)) ProvinceId = provinceId.Value;

        if (districtId == DefaultIdType.Empty) DistrictId = null;
        else if(districtId.HasValue && !DistrictId.Equals(districtId.Value)) DistrictId = districtId.Value;

        if(wardId == DefaultIdType.Empty) WardId = null;
        else if (wardId.HasValue && !WardId.Equals(wardId.Value)) WardId = wardId.Value;

        if (priceGroupId == DefaultIdType.Empty) PriceGroupId = null;
        else if (priceGroupId.HasValue && !PriceGroupId.Equals(priceGroupId.Value)) PriceGroupId = priceGroupId.Value;

        return this;
    }
}