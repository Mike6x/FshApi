using FSH.WebApi.Domain.Game;

namespace FSH.WebApi.Application.Game.VnPowers;
public class ForcastVnPowerRequest : BaseFilter, IRequest<DefaultIdType>
{
    public int? DrawId { get; set; }
    public int? RoundId { get; set; }
    public int? SubRoundId { get; set; }

    public DefaultIdType Id { get; set; }
}

public class ForcastVnPowerRequestHandler : IRequestHandler<ForcastVnPowerRequest, DefaultIdType>
{
    private readonly IRepository<VnPower> _vnPowerRepository;
    private readonly IReadRepository<VnPowerResult> _vnPowerResultRepository;
    private readonly IRepository<VnPowerForcast> _vnPowerForcastRepository;
    private readonly IStringLocalizer _t;

    public ForcastVnPowerRequestHandler(
        IRepository<VnPower> vnPowerRepository,
        IReadRepository<VnPowerResult> vnPowerResultRepository,
        IRepository<VnPowerForcast> vnPowerForcastRepository,
        IStringLocalizer<ForcastVnPowerRequestHandler> localizer)
    {
        (_vnPowerRepository, _vnPowerResultRepository, _vnPowerForcastRepository, _t)
            = (vnPowerRepository, vnPowerResultRepository, vnPowerForcastRepository, localizer);
    }

    public async Task<DefaultIdType> Handle(ForcastVnPowerRequest request, CancellationToken cancellationToken)
    {
        var entity = await _vnPowerRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);

        entity.VnPowerForcast = await _vnPowerForcastRepository.GetByIdAsync(request.Id, cancellationToken) ?? new VnPowerForcast();

        var forcastDataFilterSpec = new ForcastVnPowersSpecification(request);

        var list = await _vnPowerResultRepository.ListAsync(forcastDataFilterSpec, cancellationToken)
            ?? throw new InternalServerException(_t["Internal Error."]);
        if(list.Count == 0) { throw new NotFoundException(_t["Not found Data for Entity {0}.", request.Id]); }

        entity.VnPowerForcast = LotteryHelper.PowerForcast(list);
        entity.VnPowerForcast.Id = entity.Id;

        await _vnPowerRepository.UpdateAsync(entity, cancellationToken);

        return entity.Id;
    }
}

public class ForcastVnPowersSpecification : EntitiesByBaseFilterSpec<VnPowerResult>
{
    public ForcastVnPowersSpecification(ForcastVnPowerRequest request)
        : base(request) =>
            Query
                .OrderBy(e => e.VnPower.DrawId)
                    .Where(e => e.VnPower.DrawId <= request.DrawId, request.DrawId.HasValue)
                    .Where(e => e.RoundId == request.RoundId, request.RoundId.HasValue)
                    .Where(e => e.SubRoundId == request.SubRoundId, request.SubRoundId.HasValue);
}