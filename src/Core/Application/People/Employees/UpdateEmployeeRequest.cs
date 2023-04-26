using FSH.WebApi.Domain.People;

namespace FSH.WebApi.Application.People.Employees;

public class UpdateEmployeeRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public string Code { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public bool? IsActive { get; set; }

    public Gender Gender { get; set; }

    public DateTime? DateOfBirth { get; set; }
    public DateTime? JoiningDate { get; set; }
    public DateTime? LeavingDate { get; set; }
    public string? Description { get; set; }

    public Guid TitleId { get; set; }
    public Guid SuperiorId { get; set; }
    public Guid BusinessUnitId { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid SubDepartmentId { get; set; }
    public Guid TeamId { get; set; }
    public string? UserId { get; set; }
}

public class UpdateEmployeeRequestHandler : IRequestHandler<UpdateEmployeeRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Employee> _repository;
    private readonly IStringLocalizer _t;

    public UpdateEmployeeRequestHandler(IRepositoryWithEvents<Employee> repository, IStringLocalizer<UpdateEmployeeRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateEmployeeRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity
        ?? throw new NotFoundException(_t["Employee {0} Not Found.", request.Id]);

        entity.Update(
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
            request.TeamId,
            request.Description,
            request.UserId);

        await _repository.UpdateAsync(entity, cancellationToken);

        return request.Id;
    }
}