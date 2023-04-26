using FSH.WebApi.Application.People.Employees;
using FSH.WebApi.Domain.People;

namespace FSH.WebApi.Application.People.Titles;

public class DeleteTitleRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }

    public DeleteTitleRequest(DefaultIdType id) => Id = id;
}

public class DeleteTitleRequestHandler : IRequestHandler<DeleteTitleRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Title> _repository;
    private readonly IReadRepository<Employee> _childRepository;
    private readonly IStringLocalizer _t;

    public DeleteTitleRequestHandler(IRepositoryWithEvents<Title> repository, IReadRepository<Employee> childRepository, IStringLocalizer<DeleteTitleRequestHandler> localizer) =>
        (_repository, _childRepository, _t) = (repository, childRepository, localizer);

    public async Task<DefaultIdType> Handle(DeleteTitleRequest request, CancellationToken cancellationToken)
    {
        if (await _childRepository.AnyAsync(new EmployeesByTitleSpec(request.Id), cancellationToken))
        {
            throw new ConflictException(_t["Title cannot be deleted as it's being used."]);
        }

        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity ?? throw new NotFoundException(_t["Title {0} Not Found."]);

        await _repository.DeleteAsync(entity, cancellationToken);

        return request.Id;
    }
}
