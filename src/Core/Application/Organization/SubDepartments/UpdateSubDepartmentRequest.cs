using FSH.WebApi.Domain.People;

namespace FSH.WebApi.Application.Organization.SubDepartments;

public class UpdateSubDepartmentRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool IsActive { get; set; }
    public Guid DepartmentId { get; set; }
}

public class UpdateSubDepartmentRequestHandler : IRequestHandler<UpdateSubDepartmentRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<SubDepartment> _repository;
    private readonly IStringLocalizer _t;

    public UpdateSubDepartmentRequestHandler(IRepositoryWithEvents<SubDepartment> repository, IStringLocalizer<UpdateSubDepartmentRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateSubDepartmentRequest request, CancellationToken cancellationToken)
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
            request.DepartmentId);

        await _repository.UpdateAsync(entity, cancellationToken);

        return request.Id;
    }
}