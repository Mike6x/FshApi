using FSH.WebApi.Domain.People;

namespace FSH.WebApi.Application.People.Employees;

public class GetEmployeeRequest : IRequest<EmployeeDetailsDto>
{
    public DefaultIdType Id { get; set; }
    public GetEmployeeRequest(DefaultIdType id) => Id = id;
}

public class GetEmployeeRequestHandler : IRequestHandler<GetEmployeeRequest, EmployeeDetailsDto>
{
    private readonly IRepository<Employee> _repository;
    private readonly IStringLocalizer _t;

    public GetEmployeeRequestHandler(IRepository<Employee> repository, IStringLocalizer<GetEmployeeRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<EmployeeDetailsDto> Handle(GetEmployeeRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<Employee, EmployeeDetailsDto>)new EmployeeByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);
}