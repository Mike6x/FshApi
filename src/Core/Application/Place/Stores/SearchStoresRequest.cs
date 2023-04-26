using FSH.WebApi.Domain.Place;

namespace FSH.WebApi.Application.Place.Stores;

public class SearchStoresRequest : PaginationFilter, IRequest<PaginationResponse<StoreDto>>
{
    public Guid? RetailerId { get; set; }
}

public class SearchStoresRequestHandler : IRequestHandler<SearchStoresRequest, PaginationResponse<StoreDto>>
{
    private readonly IReadRepository<Store> _repository;

    public SearchStoresRequestHandler(IReadRepository<Store> repository) => _repository = repository;

    public async Task<PaginationResponse<StoreDto>> Handle(SearchStoresRequest request, CancellationToken cancellationToken)
    {
        var spec = new SearchStoresSpecification(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}

public class SearchStoresSpecification : EntitiesByPaginationFilterSpec<Store, StoreDto>
{
    public SearchStoresSpecification(SearchStoresRequest request)
        : base(request) =>
            Query
                .Include(e => e.Retailer)
                .Where(e => e.RetailerId.Equals(request.RetailerId!.Value), request.RetailerId.HasValue)
                .OrderBy(e => e.Order, !request.HasOrderBy());
}
