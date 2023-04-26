using FSH.WebApi.Domain.Organization;

namespace FSH.WebApi.Application.Organization.Departments;

public class CreateDepartmentRequest : IRequest<DefaultIdType>
{
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid BusinessUnitId { get; set; }
    public bool? IsActive { get; set; }
}

public class CreateDepartmentRequestHandler : IRequestHandler<CreateDepartmentRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Department> _repository;

    public CreateDepartmentRequestHandler(IRepositoryWithEvents<Department> repository) => _repository = repository;

    public async Task<DefaultIdType> Handle(CreateDepartmentRequest request, CancellationToken cancellationToken)
    {
        var entity = new Department(
                request.Order,
                request.Code,
                request.Name,
                request.Description ?? string.Empty,
                request.IsActive ?? true,
                request.BusinessUnitId);

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}
