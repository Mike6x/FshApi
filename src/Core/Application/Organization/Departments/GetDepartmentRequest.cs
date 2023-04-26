using FSH.WebApi.Domain.Organization;

namespace FSH.WebApi.Application.Organization.Departments;

public class GetDepartmentRequest : IRequest<DepartmentDetailsDto>
{
    public Guid Id { get; set; }
    public GetDepartmentRequest(Guid id) => Id = id;
}

public class GetDepartmentRequestHandler : IRequestHandler<GetDepartmentRequest, DepartmentDetailsDto>
{
    private readonly IRepository<Department> _repository;
    private readonly IStringLocalizer _t;

    public GetDepartmentRequestHandler(IRepository<Department> repository, IStringLocalizer<GetDepartmentRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<DepartmentDetailsDto> Handle(GetDepartmentRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<Department, DepartmentDetailsDto>)new DepartmentByIdWithBusinessUnitSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);
}