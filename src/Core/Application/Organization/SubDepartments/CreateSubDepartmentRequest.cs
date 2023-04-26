using FSH.WebApi.Domain.People;

namespace FSH.WebApi.Application.Organization.SubDepartments;

public class CreateSubDepartmentRequest : IRequest<DefaultIdType>
{
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid DepartmentId { get; set; }
    public bool? IsActive { get; set; }
}

public class CreateSubDepartmentRequestHandler : IRequestHandler<CreateSubDepartmentRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<SubDepartment> _repository;

    public CreateSubDepartmentRequestHandler(IRepositoryWithEvents<SubDepartment> repository) => _repository = repository;

    public async Task<DefaultIdType> Handle(CreateSubDepartmentRequest request, CancellationToken cancellationToken)
    {
        var entity = new SubDepartment(
                request.Order,
                request.Code,
                request.Name,
                request.Description ?? string.Empty,
                request.IsActive ?? true,
                request.DepartmentId);

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}
