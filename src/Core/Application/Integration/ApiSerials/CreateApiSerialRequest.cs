using FSH.WebApi.Domain.Integration;

namespace FSH.WebApi.Application.Integration.ApiSerials;

public class CreateApiSerialRequest : IRequest<DefaultIdType>
{
    public string ItemSerial { get; set; } = default!;
    public string ItemCode { get; set; } = default!;
    public string PoNumber { get; set; } = default!;
    public string PoStatus { get; set; } = default!;
    public DateTime PoCreatedDate { get; set; } = default!;
    public DateTime PoModifiedDate { get; set; } = default!;
    public string ItemName { get; set; } = default!;
    public string ItemClass { get; set; } = default!;
    public string ItemBrand { get; set; } = default!;
    public string PoProcessStatus { get; set; } = default!;
    public string CustomStatusSys { get; set; } = default!;
    public string CustomStatusIbsm { get; set; } = default!;

    public string ImportStatus { get; set; } = default!;
    public DefaultIdType? CronJobId { get; set; }
}

public class CreateApiSerialRequestHandler(IRepositoryWithEvents<ApiSerial> repository)
    : IRequestHandler<CreateApiSerialRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<ApiSerial> _repository = repository;

    public async Task<DefaultIdType> Handle(CreateApiSerialRequest request, CancellationToken cancellationToken)
    {
        var entity = new ApiSerial(
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

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}
