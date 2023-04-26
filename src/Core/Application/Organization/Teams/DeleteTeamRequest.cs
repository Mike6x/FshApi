using FSH.WebApi.Application.People.Employees;
using FSH.WebApi.Domain.Organization;
using FSH.WebApi.Domain.People;

namespace FSH.WebApi.Application.Organization.Teams;

public class DeleteTeamRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteTeamRequest(Guid id) => Id = id;
}

public class DeleteTeamRequestHandler : IRequestHandler<DeleteTeamRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Team> _repository;
    private readonly IReadRepository<Employee> _childRepository;
    private readonly IStringLocalizer _t;

    public DeleteTeamRequestHandler(IRepositoryWithEvents<Team> repository, IReadRepository<Employee> childRepository, IStringLocalizer<DeleteTeamRequestHandler> localizer) =>
        (_repository, _childRepository, _t) = (repository, childRepository, localizer);

    public async Task<Guid> Handle(DeleteTeamRequest request, CancellationToken cancellationToken)
    {
        // if (await _childRepository.AnyAsync(new EmployeesByTeamSpec(request.Id), cancellationToken))
        // {
        //    throw new ConflictException(_t["Team cannot be deleted as it's being used."]);
        // }

        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity ?? throw new NotFoundException(_t["Team {0} Not Found."]);

        await _repository.DeleteAsync(entity, cancellationToken);

        return request.Id;
    }
}
