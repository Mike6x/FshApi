using FSH.WebApi.Domain.Game;

namespace FSH.WebApi.Application.Game.VnPowerResults;
public class SearchVnPowerResultsRequest : PaginationFilter, IRequest<PaginationResponse<VnPowerResultDto>>
{
    public int? RoundId { get; set; }
    public int? SubRoundId { get; set; }
}

public class SearchVnPowerResultsRequestHandler : IRequestHandler<SearchVnPowerResultsRequest, PaginationResponse<VnPowerResultDto>>
{
    private readonly IReadRepository<VnPowerResult> _repository;

    public SearchVnPowerResultsRequestHandler(IReadRepository<VnPowerResult> repository) => _repository = repository;

    public async Task<PaginationResponse<VnPowerResultDto>> Handle(SearchVnPowerResultsRequest request, CancellationToken cancellationToken)
    {
        var spec = new SearchVnPowerResultsSpecification(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}

public class SearchVnPowerResultsSpecification : EntitiesByPaginationFilterSpec<VnPowerResult, VnPowerResultDto>
{
    public SearchVnPowerResultsSpecification(SearchVnPowerResultsRequest request)
        : base(request) =>
            Query
                .OrderByDescending(e => e.VnPower.DrawId, !request.HasOrderBy())
                    .Where(e => e.RoundId == request.RoundId, request.RoundId.HasValue)
                    .Where(e => e.RoundId == request.SubRoundId, request.SubRoundId.HasValue);
}