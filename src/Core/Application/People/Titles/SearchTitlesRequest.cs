using FSH.WebApi.Domain.People;

namespace FSH.WebApi.Application.People.Titles;

public class SearchTitlesRequest : PaginationFilter, IRequest<PaginationResponse<TitleDto>>
{
}

public class SearchTitlesRequestHandler : IRequestHandler<SearchTitlesRequest, PaginationResponse<TitleDto>>
{
    private readonly IReadRepository<Title> _repository;

    public SearchTitlesRequestHandler(IReadRepository<Title> repository) => _repository = repository;

    public async Task<PaginationResponse<TitleDto>> Handle(SearchTitlesRequest request, CancellationToken cancellationToken)
    {
        var spec = new SearchTitlesSpecification(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}

public class SearchTitlesSpecification : EntitiesByPaginationFilterSpec<Title, TitleDto>
{
    public SearchTitlesSpecification(SearchTitlesRequest request)
        : base(request) =>
            Query
                 .OrderBy(e => e.Order, !request.HasOrderBy());
}