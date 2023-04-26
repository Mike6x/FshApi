using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Production;

namespace FSH.WebApi.Application.Production.Products.EventHandlers;

public class ProductUpdatedEventHandler : EventNotificationHandler<EntityUpdatedEvent<Product>>
{
    private readonly ILogger<ProductUpdatedEventHandler> _logger;

    public ProductUpdatedEventHandler(ILogger<ProductUpdatedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityUpdatedEvent<Product> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}