using FSH.WebApi.Application.Organization.Departments;
using FSH.WebApi.Domain.Organization;

namespace FSH.WebApi.Application.Organization.BusinessUnits;

public class DeleteBusinessUnitRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteBusinessUnitRequest(Guid id) => Id = id;
}

public class DeleteBusinessUnitRequestHandler : IRequestHandler<DeleteBusinessUnitRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<BusinessUnit> _repository;
    private readonly IReadRepository<Department> _childRepository;
    private readonly IStringLocalizer _t;

    public DeleteBusinessUnitRequestHandler(IRepositoryWithEvents<BusinessUnit> repository, IReadRepository<Department> childRepository, IStringLocalizer<DeleteBusinessUnitRequestHandler> localizer) =>
        (_repository, _childRepository, _t) = (repository, childRepository, localizer);

    public async Task<Guid> Handle(DeleteBusinessUnitRequest request, CancellationToken cancellationToken)
    {
        if (await _childRepository.AnyAsync(new DepartmentsByBusinessUnitSpec(request.Id), cancellationToken))
        {
            throw new ConflictException(_t["BusinessUnit cannot be deleted as it's being used."]);
        }

        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity ?? throw new NotFoundException(_t["BusinessUnit {0} Not Found."]);

        await _repository.DeleteAsync(entity, cancellationToken);

        return request.Id;
    }
}
