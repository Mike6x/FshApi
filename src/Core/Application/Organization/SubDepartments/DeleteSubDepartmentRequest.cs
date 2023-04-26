using FSH.WebApi.Application.Organization.Teams;
using FSH.WebApi.Domain.Organization;
using FSH.WebApi.Domain.People;

namespace FSH.WebApi.Application.Organization.SubDepartments;

public class DeleteSubDepartmentRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteSubDepartmentRequest(Guid id) => Id = id;
}

public class DeleteSubDepartmentRequestHandler : IRequestHandler<DeleteSubDepartmentRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<SubDepartment> _repository;
    private readonly IReadRepository<Team> _childRepository;
    private readonly IStringLocalizer _t;

    public DeleteSubDepartmentRequestHandler(IRepositoryWithEvents<SubDepartment> repository, IReadRepository<Team> childRepository, IStringLocalizer<DeleteSubDepartmentRequestHandler> localizer) =>
        (_repository, _childRepository, _t) = (repository, childRepository, localizer);

    public async Task<Guid> Handle(DeleteSubDepartmentRequest request, CancellationToken cancellationToken)
    {
        if (await _childRepository.AnyAsync(new TeamsBySubDepartmentSpec(request.Id), cancellationToken))
        {
            throw new ConflictException(_t["SubDepartment cannot be deleted as it's being used."]);
        }

        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity ?? throw new NotFoundException(_t["SubDepartment {0} Not Found."]);

        await _repository.DeleteAsync(entity, cancellationToken);

        return request.Id;
    }
}
