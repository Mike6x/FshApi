using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Production;

namespace FSH.WebApi.Application.Production.Products.EventHandlers;

public class ProductDeletedEventHandler : EventNotificationHandler<EntityDeletedEvent<Product>>
{
    private readonly ILogger<ProductDeletedEventHandler> _logger;

    public ProductDeletedEventHandler(ILogger<ProductDeletedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityDeletedEvent<Product> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}