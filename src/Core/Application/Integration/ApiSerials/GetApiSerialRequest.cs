using FSH.WebApi.Domain.Integration;

namespace FSH.WebApi.Application.Integration.ApiSerials;

public class GetApiSerialRequest(DefaultIdType id) : IRequest<ApiSerialDetailsDto>
{
    public DefaultIdType Id { get; set; } = id;
}

public class GetApiSerialRequestHandler : IRequestHandler<GetApiSerialRequest, ApiSerialDetailsDto>
{
    private readonly IRepository<ApiSerial> _repository;
    private readonly IStringLocalizer _t;

    public GetApiSerialRequestHandler(IRepository<ApiSerial> repository, IStringLocalizer<GetApiSerialRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<ApiSerialDetailsDto> Handle(GetApiSerialRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<ApiSerial, ApiSerialDetailsDto>)new ApiSerialByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);
}