using FSH.WebApi.Domain.Organization;

namespace FSH.WebApi.Application.Organization.Departments;

public class SearchDepartmentsRequest : PaginationFilter, IRequest<PaginationResponse<DepartmentDto>>
{
    public Guid? BusinessUnitId { get; set; }
}

public class SearchDepartmentsRequestHandler : IRequestHandler<SearchDepartmentsRequest, PaginationResponse<DepartmentDto>>
{
    private readonly IReadRepository<Department> _repository;

    public SearchDepartmentsRequestHandler(IReadRepository<Department> repository) => _repository = repository;

    public async Task<PaginationResponse<DepartmentDto>> Handle(SearchDepartmentsRequest request, CancellationToken cancellationToken)
    {
        var spec = new SearchDepartmentsSpecification(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}

public class SearchDepartmentsSpecification : EntitiesByPaginationFilterSpec<Department, DepartmentDto>
{
    public SearchDepartmentsSpecification(SearchDepartmentsRequest request)
        : base(request) =>
            Query
                .Include(e => e.BusinessUnit)
                .Where(e => e.BusinessUnitId.Equals(request.BusinessUnitId!.Value), request.BusinessUnitId.HasValue)
                .OrderBy(e => e.Order, !request.HasOrderBy());
}
