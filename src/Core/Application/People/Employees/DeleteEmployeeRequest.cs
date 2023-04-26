using FSH.WebApi.Domain.People;

namespace FSH.WebApi.Application.People.Employees;

public class DeleteEmployeeRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }

    public DeleteEmployeeRequest(DefaultIdType id) => Id = id;
}

public class DeleteEmployeeRequestHandler : IRequestHandler<DeleteEmployeeRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Employee> _repository;
    private readonly IReadRepository<Employee> _childRepository;
    private readonly IStringLocalizer _t;

    public DeleteEmployeeRequestHandler(IRepositoryWithEvents<Employee> repository, IReadRepository<Employee> childRepository, IStringLocalizer<DeleteEmployeeRequestHandler> localizer) =>
        (_repository, _childRepository, _t) = (repository, childRepository, localizer);

    public async Task<DefaultIdType> Handle(DeleteEmployeeRequest request, CancellationToken cancellationToken)
    {
        if (await _childRepository.AnyAsync(new EmployeesBySuperiorSpec(request.Id), cancellationToken))
        {
            throw new ConflictException(_t["Employee cannot be deleted as it's being used."]);
        }

        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity ?? throw new NotFoundException(_t["Employee {0} Not Found."]);

        await _repository.DeleteAsync(entity, cancellationToken);

        return request.Id;
    }
}
