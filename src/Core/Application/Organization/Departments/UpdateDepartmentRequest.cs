using FSH.WebApi.Domain.Organization;

namespace FSH.WebApi.Application.Organization.Departments;

public class UpdateDepartmentRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool IsActive { get; set; }
    public Guid BusinessUnitId { get; set; }
}

public class UpdateDepartmentRequestHandler : IRequestHandler<UpdateDepartmentRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Department> _repository;
    private readonly IStringLocalizer _t;

    public UpdateDepartmentRequestHandler(IRepositoryWithEvents<Department> repository, IStringLocalizer<UpdateDepartmentRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateDepartmentRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);

        entity.Update(
            request.Order,
            request.Code,
            request.Name,
            request.Description,
            request.IsActive,
            request.BusinessUnitId);

        await _repository.UpdateAsync(entity, cancellationToken);

        return request.Id;
    }
}