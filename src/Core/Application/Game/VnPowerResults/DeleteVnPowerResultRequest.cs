using FSH.WebApi.Domain.Game;

namespace FSH.WebApi.Application.Game.VnPowerResults;

public class DeleteVnPowerResultRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }

    public DeleteVnPowerResultRequest(DefaultIdType id) => Id = id;
}

public class DeleteVnPowerResultRequestHandler : IRequestHandler<DeleteVnPowerResultRequest, DefaultIdType>
{
    private readonly IRepository<VnPowerResult> _repository;
    private readonly IStringLocalizer _t;

    public DeleteVnPowerResultRequestHandler(IRepository<VnPowerResult> repository, IStringLocalizer<DeleteVnPowerResultRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<DefaultIdType> Handle(DeleteVnPowerResultRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity ?? throw new NotFoundException(_t["entity {0} Not Found."]);

        // Add Domain Events to be raised after the commit
        // entity.DomainEvents.Add(EntityDeletedEvent.WithEntity(entity));

        await _repository.DeleteAsync(entity, cancellationToken);

        return request.Id;
    }
}