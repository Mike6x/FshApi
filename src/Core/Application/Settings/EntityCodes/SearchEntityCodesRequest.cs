using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.EntityCodes;

public class SearchEntityCodesRequest : PaginationFilter, IRequest<PaginationResponse<EntityCodeDto>>
{
    public CodeType? Type { get; set; }
}

public class SearchEntityCodesRequestHandler(IReadRepository<EntityCode> repository)
    : IRequestHandler<SearchEntityCodesRequest, PaginationResponse<EntityCodeDto>>
{
    private readonly IReadRepository<EntityCode> _repository = repository;

    public async Task<PaginationResponse<EntityCodeDto>> Handle(SearchEntityCodesRequest request, CancellationToken cancellationToken)
    {
        var spec = new SearchEntityCodesSpecification(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}

public class SearchEntityCodesSpecification : EntitiesByPaginationFilterSpec<EntityCode, EntityCodeDto>
{
    public SearchEntityCodesSpecification(SearchEntityCodesRequest request)
        : base(request) =>
            Query
                .Where(e => e.Type.Equals(request.Type!.Value), request.Type.HasValue)
                    .OrderBy(e => e.Order, !request.HasOrderBy());
}