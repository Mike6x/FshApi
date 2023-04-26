using FSH.WebApi.Domain.Organization;

namespace FSH.WebApi.Application.Organization.Teams;

public class CreateTeamRequest : IRequest<DefaultIdType>
{
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid SubDepartmentId { get; set; }
    public bool? IsActive { get; set; }
}

public class CreateTeamRequestHandler : IRequestHandler<CreateTeamRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Team> _repository;

    public CreateTeamRequestHandler(IRepositoryWithEvents<Team> repository) => _repository = repository;

    public async Task<DefaultIdType> Handle(CreateTeamRequest request, CancellationToken cancellationToken)
    {
        var entity = new Team(
                request.Order,
                request.Code,
                request.Name,
                request.Description ?? string.Empty,
                request.IsActive ?? true,
                request.SubDepartmentId);

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}
