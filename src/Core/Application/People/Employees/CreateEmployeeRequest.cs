using FSH.WebApi.Domain.People;

namespace FSH.WebApi.Application.People.Employees;

public class CreateEmployeeRequest : IRequest<DefaultIdType>
{
    public string Code { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;

    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? Address { get; set; }
    public bool IsActive { get; set; }

    public Gender Gender { get; set; }

    public DateTime DateOfBirth { get; set; }
    public DateTime JoiningDate { get; set; }
    public DateTime? LeavingDate { get; set; }

    public string? Description { get; set; }

    public DefaultIdType TitleId { get; set; }
    public DefaultIdType? SuperiorId { get; set; }
    public DefaultIdType BusinessUnitId { get; set; }
    public DefaultIdType? DepartmentId { get; set; }
    public DefaultIdType? SubDepartmentId { get; set; }
    public DefaultIdType? TeamId { get; set; }
    public string? UserId { get; set; }
}

public class CreateEmployeeRequestHandler : IRequestHandler<CreateEmployeeRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Employee> _repository;

    public CreateEmployeeRequestHandler(IRepositoryWithEvents<Employee> repository) => _repository = repository;

    public async Task<DefaultIdType> Handle(CreateEmployeeRequest request, CancellationToken cancellationToken)
    {
        var entity = new Employee(
        request.Code,
        request.FirstName,
        request.LastName,
        request.PhoneNumber,
        request.Email,
        request.Address,
        request.IsActive,
        request.Gender,
        request.DateOfBirth,
        request.JoiningDate,
        request.LeavingDate,
        request.TitleId,
        request.SuperiorId,
        request.BusinessUnitId,
        request.DepartmentId,
        request.SubDepartmentId,
        request.SubDepartmentId,
        request.Description,
        request.UserId);

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}
