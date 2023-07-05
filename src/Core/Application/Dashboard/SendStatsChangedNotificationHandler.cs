using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Communication;
using FSH.WebApi.Domain.Identity;
using FSH.WebApi.Domain.Production;
using FSH.WebApi.Domain.Property;
using FSH.WebApi.Shared.Events;

namespace FSH.WebApi.Application.Dashboard;

public class SendStatsChangedNotificationHandler :
    IEventNotificationHandler<EntityCreatedEvent<Brand>>,
    IEventNotificationHandler<EntityDeletedEvent<Brand>>,
    IEventNotificationHandler<EntityCreatedEvent<BusinessLine>>,
    IEventNotificationHandler<EntityDeletedEvent<BusinessLine>>,
    IEventNotificationHandler<EntityCreatedEvent<GroupCategorie>>,
    IEventNotificationHandler<EntityDeletedEvent<GroupCategorie>>,
    IEventNotificationHandler<EntityCreatedEvent<Categorie>>,
    IEventNotificationHandler<EntityDeletedEvent<Categorie>>,
    IEventNotificationHandler<EntityCreatedEvent<SubCategorie>>,
    IEventNotificationHandler<EntityDeletedEvent<SubCategorie>>,

    IEventNotificationHandler<EntityCreatedEvent<Product>>,
    IEventNotificationHandler<EntityDeletedEvent<Product>>,
    IEventNotificationHandler<EntityCreatedEvent<Asset>>,
    IEventNotificationHandler<EntityDeletedEvent<Asset>>,

    IEventNotificationHandler<ChatMessageCreatedEvent>,

    IEventNotificationHandler<ApplicationRoleCreatedEvent>,
    IEventNotificationHandler<ApplicationRoleDeletedEvent>,
    IEventNotificationHandler<ApplicationUserCreatedEvent>
{
    private readonly ILogger<SendStatsChangedNotificationHandler> _logger;
    private readonly INotificationSender _notifications;

    public SendStatsChangedNotificationHandler(ILogger<SendStatsChangedNotificationHandler> logger, INotificationSender notifications) =>
        (_logger, _notifications) = (logger, notifications);

    public Task Handle(EventNotification<EntityCreatedEvent<Brand>> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);
    public Task Handle(EventNotification<EntityDeletedEvent<Brand>> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);
    public Task Handle(EventNotification<EntityCreatedEvent<BusinessLine>> notification, CancellationToken cancellationToken) =>
    SendStatsChangedNotification(notification.Event, cancellationToken);
    public Task Handle(EventNotification<EntityDeletedEvent<BusinessLine>> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);
    public Task Handle(EventNotification<EntityCreatedEvent<GroupCategorie>> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);
    public Task Handle(EventNotification<EntityDeletedEvent<GroupCategorie>> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);
    public Task Handle(EventNotification<EntityCreatedEvent<Categorie>> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);
    public Task Handle(EventNotification<EntityDeletedEvent<Categorie>> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);
    public Task Handle(EventNotification<EntityCreatedEvent<SubCategorie>> notification, CancellationToken cancellationToken) =>
    SendStatsChangedNotification(notification.Event, cancellationToken);
    public Task Handle(EventNotification<EntityDeletedEvent<SubCategorie>> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);

    public Task Handle(EventNotification<EntityCreatedEvent<Product>> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);
    public Task Handle(EventNotification<EntityDeletedEvent<Product>> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);

    public Task Handle(EventNotification<EntityCreatedEvent<Asset>> notification, CancellationToken cancellationToken) =>
    SendStatsChangedNotification(notification.Event, cancellationToken);
    public Task Handle(EventNotification<EntityDeletedEvent<Asset>> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);

    public Task Handle(EventNotification<ChatMessageCreatedEvent> notification, CancellationToken cancellationToken) =>
    SendStatsChangedNotification(notification.Event, cancellationToken);

    public Task Handle(EventNotification<ApplicationRoleCreatedEvent> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);
    public Task Handle(EventNotification<ApplicationRoleDeletedEvent> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);
    public Task Handle(EventNotification<ApplicationUserCreatedEvent> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);

    private Task SendStatsChangedNotification(IEvent @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered => Sending StatsChangedNotification", @event.GetType().Name);

        return _notifications.SendToAllAsync(new StatsChangedNotification(), cancellationToken);
    }
}