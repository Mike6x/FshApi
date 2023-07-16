using FSH.WebApi.Application.Game.VnPowerResults;
using FSH.WebApi.Domain.Game;

namespace FSH.WebApi.Application.Game.VnPowers;

public class GetVnPowerResultsRequest : BaseFilter, IRequest<List<VnPowerResultDto>>
{
    public int? RoundId { get; set; }
    public int? SubRoundId { get; set; }
}

public class GetVnPowerResultsRequestHandler : IRequestHandler<GetVnPowerResultsRequest, List<VnPowerResultDto>>
{
    private readonly IRepository<VnPowerResult> _repository;
    public GetVnPowerResultsRequestHandler(IRepository<VnPowerResult> repository)
        => _repository = repository;

    public async Task<List<VnPowerResultDto>> Handle(GetVnPowerResultsRequest request, CancellationToken cancellationToken)
    {
        var spec = new GetVnPowerResultsSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);
        return list ?? new List<VnPowerResultDto>();
    }
}

public class GetVnPowerResultsSpecification : EntitiesByBaseFilterSpec<VnPowerResult, VnPowerResultDto>
{
    public GetVnPowerResultsSpecification(GetVnPowerResultsRequest request)
        : base(request) =>
            Query
                .OrderBy(e => e.RoundId)
                    .Where(e => e.RoundId == request.RoundId, request.RoundId.HasValue)
                    .Where(e => e.RoundId == request.SubRoundId, request.SubRoundId.HasValue);
}
