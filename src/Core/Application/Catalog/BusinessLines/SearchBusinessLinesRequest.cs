namespace FSH.WebApi.Application.Catalog.BusinessLines;

public class SearchBusinessLinesRequest : PaginationFilter, IRequest<PaginationResponse<BusinessLineDto>>
{
}

public class SearchBusinessLinesRequestHandler : IRequestHandler<SearchBusinessLinesRequest, PaginationResponse<BusinessLineDto>>
{
    private readonly IReadRepository<BusinessLine> _repository;

    public SearchBusinessLinesRequestHandler(IReadRepository<BusinessLine> repository) => _repository = repository;

    public async Task<PaginationResponse<BusinessLineDto>> Handle(SearchBusinessLinesRequest request, CancellationToken cancellationToken)
    {
        var spec = new SearchBusinessLinesSpecification(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}

public class SearchBusinessLinesSpecification : EntitiesByPaginationFilterSpec<BusinessLine, BusinessLineDto>
{
    public SearchBusinessLinesSpecification(SearchBusinessLinesRequest request)
        : base(request) =>
            Query
                .OrderBy(e => e.Order, !request.HasOrderBy());
}