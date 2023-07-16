using FSH.WebApi.Domain.Integration;

namespace FSH.WebApi.Application.Integration.ApiSerials;

public class UpdateApiSerialRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }
    public string? ItemSerial { get; set; }
    public string? ItemCode { get; set; }
    public string? PoNumber { get; set; }
    public string? PoStatus { get; set; }
    public DateTime? PoCreatedDate { get; set; }
    public DateTime? PoModifiedDate { get; set; }
    public string? ItemName { get; set; }
    public string? ItemClass { get; set; }
    public string? ItemBrand { get; set; }
    public string? PoProcessStatus { get; set; }
    public string? CustomStatusSys { get; set; }
    public string? CustomStatusIbsm { get; set; }

    public string ImportStatus { get; set; } = default!;
    public DefaultIdType CronJobId { get; set; }
}

public class UpdateApiSerialRequestHandler : IRequestHandler<UpdateApiSerialRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<ApiSerial> _repository;
    private readonly IStringLocalizer _t;

    public UpdateApiSerialRequestHandler(IRepositoryWithEvents<ApiSerial> repository, IStringLocalizer<UpdateApiSerialRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<DefaultIdType> Handle(UpdateApiSerialRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);

        entity.Update(
            request.ItemSerial,
            request.ItemCode,
            request.ItemName,
            request.ItemClass,
            request.ItemBrand,
            request.PoNumber,
            request.PoStatus,
            request.PoCreatedDate,
            request.PoModifiedDate,
            request.PoProcessStatus,
            request.CustomStatusSys,
            request.CustomStatusIbsm,
            request.ImportStatus,
            request.CronJobId);

        await _repository.UpdateAsync(entity, cancellationToken);

        return request.Id;
    }
}