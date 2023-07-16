using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Game;

namespace FSH.WebApi.Application.Game.VnPowers;

public class DeleteVnPowerRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }

    public DeleteVnPowerRequest(DefaultIdType id) => Id = id;
}

public class DeleteVnPowerRequestHandler : IRequestHandler<DeleteVnPowerRequest, DefaultIdType>
{
    private readonly IRepository<VnPower> _repository;
    private readonly IStringLocalizer _t;

    public DeleteVnPowerRequestHandler(IRepository<VnPower> repository, IStringLocalizer<DeleteVnPowerRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<DefaultIdType> Handle(DeleteVnPowerRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity ?? throw new NotFoundException(_t["entity {0} Not Found."]);

        // Add Domain Events to be raised after the commit
        // entity.DomainEvents.Add(EntityDeletedEvent.WithEntity(entity));

        await _repository.DeleteAsync(entity, cancellationToken);

        return request.Id;
    }
}