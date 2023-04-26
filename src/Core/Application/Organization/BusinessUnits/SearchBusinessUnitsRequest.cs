using FSH.WebApi.Domain.Organization;

namespace FSH.WebApi.Application.Organization.BusinessUnits;

public class SearchBusinessUnitsRequest : PaginationFilter, IRequest<PaginationResponse<BusinessUnitDto>>
{
}

public class SearchBusinessUnitsRequestHandler : IRequestHandler<SearchBusinessUnitsRequest, PaginationResponse<BusinessUnitDto>>
{
    private readonly IReadRepository<BusinessUnit> _repository;

    public SearchBusinessUnitsRequestHandler(IReadRepository<BusinessUnit> repository) => _repository = repository;

    public async Task<PaginationResponse<BusinessUnitDto>> Handle(SearchBusinessUnitsRequest request, CancellationToken cancellationToken)
    {
        var spec = new SearchBusinessUnitsSpecification(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}

public class SearchBusinessUnitsSpecification : EntitiesByPaginationFilterSpec<BusinessUnit, BusinessUnitDto>
{
    public SearchBusinessUnitsSpecification(SearchBusinessUnitsRequest request)
        : base(request) =>
            Query
                .OrderBy(e => e.Order, !request.HasOrderBy());
}
