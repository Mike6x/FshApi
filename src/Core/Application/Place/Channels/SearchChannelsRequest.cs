using FSH.WebApi.Domain.Place;

namespace FSH.WebApi.Application.Place.Channels;

public class SearchChannelsRequest : PaginationFilter, IRequest<PaginationResponse<ChannelDto>>
{
}

public class SearchChannelsRequestHandler : IRequestHandler<SearchChannelsRequest, PaginationResponse<ChannelDto>>
{
    private readonly IReadRepository<Channel> _repository;

    public SearchChannelsRequestHandler(IReadRepository<Channel> repository) => _repository = repository;

    public async Task<PaginationResponse<ChannelDto>> Handle(SearchChannelsRequest request, CancellationToken cancellationToken)
    {
        var spec = new SearchChannelsSpecification(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}

public class SearchChannelsSpecification : EntitiesByPaginationFilterSpec<Channel, ChannelDto>
{
    public SearchChannelsSpecification(SearchChannelsRequest request)
        : base(request) =>
            Query
                .OrderBy(e => e.Order, !request.HasOrderBy());
}
