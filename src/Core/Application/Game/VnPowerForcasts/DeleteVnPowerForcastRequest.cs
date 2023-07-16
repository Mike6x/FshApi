using FSH.WebApi.Domain.Game;

namespace FSH.WebApi.Application.Game.VnPowerForcasts;

public class DeleteVnPowerForcastRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }

    public DeleteVnPowerForcastRequest(DefaultIdType id) => Id = id;
}

public class DeleteVnPowerForcastRequestHandler : IRequestHandler<DeleteVnPowerForcastRequest, DefaultIdType>
{
    private readonly IRepository<VnPowerForcast> _repository;
    private readonly IStringLocalizer _t;

    public DeleteVnPowerForcastRequestHandler(IRepository<VnPowerForcast> repository, IStringLocalizer<DeleteVnPowerForcastRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<DefaultIdType> Handle(DeleteVnPowerForcastRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity ?? throw new NotFoundException(_t["entity {0} Not Found."]);

        // Add Domain Events to be raised after the commit
        // entity.DomainEvents.Add(EntityDeletedEvent.WithEntity(entity));

        await _repository.DeleteAsync(entity, cancellationToken);

        return request.Id;
    }
}