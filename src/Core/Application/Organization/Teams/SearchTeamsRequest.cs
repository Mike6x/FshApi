using FSH.WebApi.Domain.Organization;

namespace FSH.WebApi.Application.Organization.Teams;

public class SearchTeamsRequest : PaginationFilter, IRequest<PaginationResponse<TeamDto>>
{
    public Guid? SubDepartmentId { get; set; }
}

public class SearchTeamsRequestHandler : IRequestHandler<SearchTeamsRequest, PaginationResponse<TeamDto>>
{
    private readonly IReadRepository<Team> _repository;

    public SearchTeamsRequestHandler(IReadRepository<Team> repository) => _repository = repository;

    public async Task<PaginationResponse<TeamDto>> Handle(SearchTeamsRequest request, CancellationToken cancellationToken)
    {
        var spec = new SearchTeamsSpecification(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}

public class SearchTeamsSpecification : EntitiesByPaginationFilterSpec<Team, TeamDto>
{
    public SearchTeamsSpecification(SearchTeamsRequest request)
        : base(request) =>
            Query
                .Include(e => e.SubDepartment)
                .Where(e => e.SubDepartmentId.Equals(request.SubDepartmentId!.Value), request.SubDepartmentId.HasValue)
                .OrderBy(e => e.Order, !request.HasOrderBy());
}

