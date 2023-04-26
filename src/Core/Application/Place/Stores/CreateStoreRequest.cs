using FSH.WebApi.Domain.Place;

namespace FSH.WebApi.Application.Place.Stores;

public class CreateStoreRequest : IRequest<DefaultIdType>
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

    public DefaultIdType RetailerId { get; set; }
    public DefaultIdType ProvinceId { get; set; }
    public DefaultIdType? DistrictId { get; set; }
    public DefaultIdType? WardId { get; set; }
}

public class CreateStoreRequestHandler : IRequestHandler<CreateStoreRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Store> _repository;

    public CreateStoreRequestHandler(IRepositoryWithEvents<Store> repository) => _repository = repository;

    public async Task<DefaultIdType> Handle(CreateStoreRequest request, CancellationToken cancellationToken)
    {
        var entity = new Store(
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
                request.RetailerId,
                request.ProvinceId,
                request.DistrictId,
                request.WardId);

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}