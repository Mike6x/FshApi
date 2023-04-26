using FSH.WebApi.Domain.Place;

namespace FSH.WebApi.Application.Place.Stores;

public class UpdateStoreRequest : IRequest<Guid>
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

    public DefaultIdType RetailerId { get; set; }
    public DefaultIdType ProvinceId { get; set; }
    public DefaultIdType DistrictId { get; set; }
    public DefaultIdType WardId { get; set; }
}

public class UpdateStoreRequestHandler : IRequestHandler<UpdateStoreRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Store> _repository;
    private readonly IStringLocalizer _t;

    public UpdateStoreRequestHandler(IRepositoryWithEvents<Store> repository, IStringLocalizer<UpdateStoreRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateStoreRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);

        entity.Update(
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

        await _repository.UpdateAsync(entity, cancellationToken);

        return request.Id;
    }
}