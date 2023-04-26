using FSH.WebApi.Domain.Organization;

namespace FSH.WebApi.Application.Organization.Teams;

public class UpdateTeamRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool IsActive { get; set; }
    public Guid SubDepartmentId { get; set; }
}

public class UpdateTeamRequestHandler : IRequestHandler<UpdateTeamRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Team> _repository;
    private readonly IStringLocalizer _t;

    public UpdateTeamRequestHandler(IRepositoryWithEvents<Team> repository, IStringLocalizer<UpdateTeamRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateTeamRequest request, CancellationToken cancellationToken)
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
            request.SubDepartmentId);

        await _repository.UpdateAsync(entity, cancellationToken);

        return request.Id;
    }
}