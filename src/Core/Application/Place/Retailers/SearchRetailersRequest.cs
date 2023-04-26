using FSH.WebApi.Domain.Place;

namespace FSH.WebApi.Application.Place.Retailers;

public class SearchRetailersRequest : PaginationFilter, IRequest<PaginationResponse<RetailerDto>>
{
    public Guid? ChannelId { get; set; }
}

public class SearchRetailersRequestHandler : IRequestHandler<SearchRetailersRequest, PaginationResponse<RetailerDto>>
{
    private readonly IReadRepository<Retailer> _repository;

    public SearchRetailersRequestHandler(IReadRepository<Retailer> repository) => _repository = repository;

    public async Task<PaginationResponse<RetailerDto>> Handle(SearchRetailersRequest request, CancellationToken cancellationToken)
    {
        var spec = new SearchRetailersSpecification(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}

public class SearchRetailersSpecification : EntitiesByPaginationFilterSpec<Retailer, RetailerDto>
{
    public SearchRetailersSpecification(SearchRetailersRequest request)
        : base(request) =>
            Query
                .Include(e => e.Channel)
                .Where(e => e.ChannelId.Equals(request.ChannelId!.Value), request.ChannelId.HasValue)
                .OrderBy(e => e.Order, !request.HasOrderBy());
}
