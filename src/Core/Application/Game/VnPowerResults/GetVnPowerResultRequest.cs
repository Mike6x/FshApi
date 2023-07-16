using FSH.WebApi.Domain.Game;

namespace FSH.WebApi.Application.Game.VnPowerResults;

public class GetVnPowerResultRequest : IRequest<VnPowerResultDetailsDto>
{
    public DefaultIdType Id { get; set; }
    public GetVnPowerResultRequest(DefaultIdType id) => Id = id;
}

public class GetVnPowerResultRequestHandler : IRequestHandler<GetVnPowerResultRequest, VnPowerResultDetailsDto>
{
    private readonly IRepository<VnPowerResult> _repository;
    private readonly IStringLocalizer _t;

    public GetVnPowerResultRequestHandler(IRepository<VnPowerResult> repository, IStringLocalizer<GetVnPowerResultRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<VnPowerResultDetailsDto> Handle(GetVnPowerResultRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<VnPowerResult, VnPowerResultDetailsDto>)new VnPowerResultByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);
}