using FSH.WebApi.Domain.Game;

namespace FSH.WebApi.Application.Game.VnPowerForcasts;
public class SearchVnPowerForcastsRequest : PaginationFilter, IRequest<PaginationResponse<VnPowerForcastDto>>
{
}

public class SearchVnPowerForcastsRequestHandler : IRequestHandler<SearchVnPowerForcastsRequest, PaginationResponse<VnPowerForcastDto>>
{
    private readonly IReadRepository<VnPowerForcast> _repository;

    public SearchVnPowerForcastsRequestHandler(IReadRepository<VnPowerForcast> repository) => _repository = repository;

    public async Task<PaginationResponse<VnPowerForcastDto>> Handle(SearchVnPowerForcastsRequest request, CancellationToken cancellationToken)
    {
        var spec = new SearchVnPowerForcastsSpecification(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}

public class SearchVnPowerForcastsSpecification : EntitiesByPaginationFilterSpec<VnPowerForcast, VnPowerForcastDto>
{
    public SearchVnPowerForcastsSpecification(SearchVnPowerForcastsRequest request)
        : base(request) =>
            Query
                .OrderByDescending(e => e.VnPower.DrawId, !request.HasOrderBy());
}