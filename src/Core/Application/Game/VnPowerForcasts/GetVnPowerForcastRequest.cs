using FSH.WebApi.Domain.Game;

namespace FSH.WebApi.Application.Game.VnPowerForcasts;

public class GetVnPowerForcastRequest : IRequest<VnPowerForcastDetailsDto>
{
    public DefaultIdType Id { get; set; }
    public GetVnPowerForcastRequest(DefaultIdType id) => Id = id;
}

public class GetVnPowerForcastRequestHandler : IRequestHandler<GetVnPowerForcastRequest, VnPowerForcastDetailsDto>
{
    private readonly IRepository<VnPowerForcast> _repository;
    private readonly IStringLocalizer _t;

    public GetVnPowerForcastRequestHandler(IRepository<VnPowerForcast> repository, IStringLocalizer<GetVnPowerForcastRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<VnPowerForcastDetailsDto> Handle(GetVnPowerForcastRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<VnPowerForcast, VnPowerForcastDetailsDto>)new VnPowerForcastByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);
}