using FSH.WebApi.Domain.Production;

namespace FSH.WebApi.Application.Production.Products;

public class SearchProductsRequest : PaginationFilter, IRequest<PaginationResponse<ProductDto>>
{
    public DefaultIdType? BrandId { get; set; }
    public DefaultIdType? CategorieId { get; set; }
    public DefaultIdType? SubCategorieId { get; set; }
    public DefaultIdType? VendorId { get; set; }

    public decimal? MinimumRate { get; set; }
    public decimal? MaximumRate { get; set; }
}

public class SearchProductsRequestHandler : IRequestHandler<SearchProductsRequest, PaginationResponse<ProductDto>>
{
    private readonly IReadRepository<Product> _repository;

    public SearchProductsRequestHandler(IReadRepository<Product> repository) => _repository = repository;

    public async Task<PaginationResponse<ProductDto>> Handle(SearchProductsRequest request, CancellationToken cancellationToken)
    {
        var spec = new SearchProductsSpecification(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}

public class SearchProductsSpecification : EntitiesByPaginationFilterSpec<Product, ProductDto>
{
    public SearchProductsSpecification(SearchProductsRequest request)
        : base(request) =>
        Query
            .Include(e => e.Brand)
            .OrderBy(e => e.Order, !request.HasOrderBy())
                .Where(e => e.BrandId.Equals(request.BrandId!.Value), request.BrandId.HasValue)
                .Where(e => e.CategorieId.Equals(request.CategorieId!.Value), request.CategorieId.HasValue)
                .Where(e => e.SubCategorieId.Equals(request.SubCategorieId!.Value), request.SubCategorieId.HasValue)
                .Where(e => e.VendorId.Equals(request.VendorId!.Value), request.VendorId.HasValue)
                .Where(e => e.ListPrice >= request.MinimumRate!.Value, request.MinimumRate.HasValue)
                .Where(e => e.ListPrice <= request.MaximumRate!.Value, request.MaximumRate.HasValue);
}