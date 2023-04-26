namespace FSH.WebApi.Application.Catalog.BusinessLines;

public class UpdateBusinessLineRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}

public class UpdateBusinessLineRequestHandler : IRequestHandler<UpdateBusinessLineRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<BusinessLine> _repository;
    private readonly IStringLocalizer _t;

    public UpdateBusinessLineRequestHandler(IRepositoryWithEvents<BusinessLine> repository, IStringLocalizer<UpdateBusinessLineRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateBusinessLineRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);

        entity.Update(
            request.Order,
            request.Code,
            request.Name,
            request.Description,
            request.IsActive);

        await _repository.UpdateAsync(entity, cancellationToken);

        return request.Id;
    }
}