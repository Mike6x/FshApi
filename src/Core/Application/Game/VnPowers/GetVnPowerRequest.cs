using FSH.WebApi.Domain.Game;

namespace FSH.WebApi.Application.Game.VnPowers;

public class GetVnPowerRequest : IRequest<VnPowerDetailsDto>
{
    public DefaultIdType Id { get; set; }
    public GetVnPowerRequest(DefaultIdType id) => Id = id;
}

public class GetVnPowerRequestHandler : IRequestHandler<GetVnPowerRequest, VnPowerDetailsDto>
{
    private readonly IRepository<VnPower> _repository;
    private readonly IStringLocalizer _t;

    public GetVnPowerRequestHandler(IRepository<VnPower> repository, IStringLocalizer<GetVnPowerRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<VnPowerDetailsDto> Handle(GetVnPowerRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<VnPower, VnPowerDetailsDto>)new VnPowerByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);
}