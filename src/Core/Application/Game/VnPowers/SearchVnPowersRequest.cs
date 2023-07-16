using FSH.WebApi.Domain.Game;

namespace FSH.WebApi.Application.Game.VnPowers;
public class SearchVnPowersRequest : PaginationFilter, IRequest<PaginationResponse<VnPowerDto>>
{
}

public class SearchVnPowersRequestHandler : IRequestHandler<SearchVnPowersRequest, PaginationResponse<VnPowerDto>>
{
    private readonly IReadRepository<VnPower> _repository;

    public SearchVnPowersRequestHandler(IReadRepository<VnPower> repository) => _repository = repository;

    public async Task<PaginationResponse<VnPowerDto>> Handle(SearchVnPowersRequest request, CancellationToken cancellationToken)
    {
        var spec = new SearchVnPowersSpecification(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}

public class SearchVnPowersSpecification : EntitiesByPaginationFilterSpec<VnPower, VnPowerDto>
{
    public SearchVnPowersSpecification(SearchVnPowersRequest request)
        : base(request) =>
            Query
                .OrderByDescending(e => e.DrawId, !request.HasOrderBy());
}