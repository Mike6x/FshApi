using FSH.WebApi.Domain.People;

namespace FSH.WebApi.Application.Organization.SubDepartments;

public class SearchSubDepartmentsRequest : PaginationFilter, IRequest<PaginationResponse<SubDepartmentDto>>
{
    public Guid? DepartmentId { get; set; }
}

public class SearchSubDepartmentsRequestHandler : IRequestHandler<SearchSubDepartmentsRequest, PaginationResponse<SubDepartmentDto>>
{
    private readonly IReadRepository<SubDepartment> _repository;

    public SearchSubDepartmentsRequestHandler(IReadRepository<SubDepartment> repository) => _repository = repository;

    public async Task<PaginationResponse<SubDepartmentDto>> Handle(SearchSubDepartmentsRequest request, CancellationToken cancellationToken)
    {
        var spec = new SearchSubDepartmentsSpecification(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}

public class SearchSubDepartmentsSpecification : EntitiesByPaginationFilterSpec<SubDepartment, SubDepartmentDto>
{
    public SearchSubDepartmentsSpecification(SearchSubDepartmentsRequest request)
        : base(request) =>
            Query
                .Include(e => e.Department)
                .Where(e => e.DepartmentId.Equals(request.DepartmentId!.Value), request.DepartmentId.HasValue)
                .OrderBy(e => e.Order, !request.HasOrderBy());
}