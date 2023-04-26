using FSH.WebApi.Domain.Place;

namespace FSH.WebApi.Application.Place.Retailers;

public class CreateRetailerRequest : IRequest<DefaultIdType>
{
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
    public DefaultIdType ProvinceId { get; set; }
    public DefaultIdType? DistrictId { get; set; }
    public DefaultIdType? WardId { get; set; }

    public DefaultIdType? PriceGroupId { get; set; } // Mã nhóm giá
}

public class CreateRetailerRequestHandler : IRequestHandler<CreateRetailerRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Retailer> _repository;

    public CreateRetailerRequestHandler(IRepositoryWithEvents<Retailer> repository) => _repository = repository;

    public async Task<DefaultIdType> Handle(CreateRetailerRequest request, CancellationToken cancellationToken)
    {
        var entity = new Retailer(
                request.Order,
                request.Code,
                request.Name,
                request.Description,
                request.IsActive,
                request.FullName,
                request.Latitude,
                request.Longitude,
                request.Address,
                request.Email,
                request.Phone,
                request.Fax,
                request.TaxCode,
                request.Grade,
                request.SellGroup,
                request.ChannelId,
                request.ProvinceId,
                request.DistrictId,
                request.WardId,
                request.PriceGroupId);

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}