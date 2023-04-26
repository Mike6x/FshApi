using FSH.WebApi.Application.Organization.SubDepartments;
using FSH.WebApi.Domain.Organization;
using FSH.WebApi.Domain.People;

namespace FSH.WebApi.Application.Organization.Departments;

public class DeleteDepartmentRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteDepartmentRequest(Guid id) => Id = id;
}

public class DeleteDepartmentRequestHandler : IRequestHandler<DeleteDepartmentRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Department> _repository;
    private readonly IReadRepository<SubDepartment> _childRepository;
    private readonly IStringLocalizer _t;

    public DeleteDepartmentRequestHandler(IRepositoryWithEvents<Department> repository, IReadRepository<SubDepartment> childRepository, IStringLocalizer<DeleteDepartmentRequestHandler> localizer) =>
        (_repository, _childRepository, _t) = (repository, childRepository, localizer);

    public async Task<Guid> Handle(DeleteDepartmentRequest request, CancellationToken cancellationToken)
    {
        if (await _childRepository.AnyAsync(new SubDepartmentsByDepartmentSpec(request.Id), cancellationToken))
        {
            throw new ConflictException(_t["Department cannot be deleted as it's being used."]);
        }

        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity ?? throw new NotFoundException(_t["Department {0} Not Found."]);

        await _repository.DeleteAsync(entity, cancellationToken);

        return request.Id;
    }
}
