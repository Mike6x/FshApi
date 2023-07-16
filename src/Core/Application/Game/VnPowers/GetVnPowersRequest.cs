using FSH.WebApi.Domain.Game;

namespace FSH.WebApi.Application.Game.VnPowers;

public class GetVnPowersRequest : BaseFilter, IRequest<List<VnPowerDto>>
{
    public int? DrawId { get; set; }
    public int? RoundId { get; set; }
    public int? SubRoundId { get; set; }
}

public class GetVnPowersRequestHandler : IRequestHandler<GetVnPowersRequest, List<VnPowerDto>>
{
    private readonly IRepository<VnPower> _repository;
    public GetVnPowersRequestHandler(IRepository<VnPower> repository)
        => _repository = repository;

    public async Task<List<VnPowerDto>> Handle(GetVnPowersRequest request, CancellationToken cancellationToken)
    {
        var spec = new GetVnPowersSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return list ?? new List<VnPowerDto>();
    }
}

public class GetVnPowersSpecification : EntitiesByBaseFilterSpec<VnPower, VnPowerDto>
{
    public GetVnPowersSpecification(GetVnPowersRequest request)
        : base(request) =>
            Query
                .OrderBy(e => e.DrawId)
                    .Where(e => e.DrawId <= request.DrawId, request.DrawId.HasValue)
                    .Where(e => e.VnPowerResult.RoundId == request.RoundId, request.RoundId.HasValue)
                    .Where(e => e.VnPowerResult.SubRoundId == request.SubRoundId, request.SubRoundId.HasValue)
                    .Where(e => e.DrawId > 0);
}