using FSH.WebApi.Domain.People;

namespace FSH.WebApi.Application.People.Employees;

public class SearchEmployeesRequest : PaginationFilter, IRequest<PaginationResponse<EmployeeDto>>
{
    // public DefaultIdType? TitleId { get; set; }
    public Gender? Gender { get; set; }
    public int? EmployeeGrade { get; set; } = 0;
    public DefaultIdType? BusinessUnitId { get; set; }
    public DefaultIdType? DepartmentId { get; set; }
    public DefaultIdType? SubDepartmentId { get; set; }
    public DefaultIdType? TeamId { get; set; }
}

public class SearchEmployeesRequestHandler(IReadRepository<Employee> repository)
    : IRequestHandler<SearchEmployeesRequest, PaginationResponse<EmployeeDto>>
{
    private readonly IReadRepository<Employee> _repository = repository;

    public async Task<PaginationResponse<EmployeeDto>> Handle(SearchEmployeesRequest request, CancellationToken cancellationToken)
    {
        var spec = new EmployeesBySearchRequestWithSpec(request);

        // var res = await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}

public class EmployeesBySearchRequestWithSpec : EntitiesByPaginationFilterSpec<Employee, EmployeeDto>
{
    public EmployeesBySearchRequestWithSpec(SearchEmployeesRequest request)
        : base(request) =>
            Query
                .Where(e => e.Title.Value > request.EmployeeGrade!, request.EmployeeGrade.HasValue)
                .Where(e => e.BusinessUnitId.Equals(request.BusinessUnitId!.Value), request.BusinessUnitId.HasValue)
                .Where(e => e.DepartmentId.Equals(request.DepartmentId!.Value), request.DepartmentId.HasValue)
                .Where(e => e.SubDepartmentId.Equals(request.SubDepartmentId!.Value), request.SubDepartmentId.HasValue)
                .Where(e => e.TeamId.Equals(request.TeamId!.Value), request.TeamId.HasValue)
                .Where(e => e.Gender.Equals(request.Gender!), request.Gender.HasValue)
                    .OrderBy(e => e.FirstName, !request.HasOrderBy());
}