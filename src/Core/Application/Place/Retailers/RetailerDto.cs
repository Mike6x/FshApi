namespace FSH.WebApi.Application.Place.Retailers;

public class RetailerDto : IDto
{
    public Guid Id { get; set; }
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public string? FullName { get; set; }
    public string? Latitude { get; set; } // Vĩ độ
    public string? Longitude { get; set; } // Kinh độ
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Fax { get; set; }
    public string? TaxCode { get; set; }
    public int Grade { get; set; } // Dai ly cap 1,2,3
    public string SellGroup { get; set; } = default!;

    public DefaultIdType ChannelId { get; set; }
    public string ChannelName { get; set; } = default!;

    public DefaultIdType ProvinceId { get; set; }
    public string ProvinceName { get; set; } = default!;
    public DefaultIdType? DistrictId { get; set; }
    public string? DistrictName { get; set; }
    public DefaultIdType? WardId { get; set; }
    public string? WardName { get; set; }

    public DefaultIdType? PriceGroupId { get; set; } // Mã nhóm giá
    public string? PriceGroupName { get; set; }
}

public class RetailerDetailsDto : IDto
{
    public Guid Id { get; set; }

    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}

public class RetailerExportDto : IDto
{
    public Guid Id { get; set; }
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public string? FullName { get; set; }
    public string? Latitude { get; set; } // Vĩ độ
    public string? Longitude { get; set; } // Kinh độ
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Fax { get; set; }
    public string? TaxCode { get; set; }
    public int Grade { get; set; } // Dai ly cap 1,2,3

    public string SellGroup { get; set; } = default!;

    public DefaultIdType ChannelId { get; set; }
    public string ChannelName { get; set; } = default!;

    public DefaultIdType ProvinceId { get; set; }
    public string ProvinceName { get; set; } = default!;
    public DefaultIdType? DistrictId { get; set; }
    public string? DistrictName { get; set; }
    public DefaultIdType? WardId { get; set; }
    public string? WardName { get; set; }
    public DefaultIdType? PriceGroupId { get; set; } // Mã nhóm giá
    public string? PriceGroupName { get; set; }

    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}