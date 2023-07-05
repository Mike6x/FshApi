//using FSH.WebApi.Application.Common.Events;
//using FSH.WebApi.Domain.Common.Events;
//using Microsoft.Extensions.Logging;

//namespace FSH.WebApi.Infrastructure.Chat.EventHandlers;

//public class ChatMessageCreatedEventHandler : EventNotificationHandler<EntityCreatedEvent<ChatMessage>>
//{
//    private readonly ILogger<ChatMessageCreatedEventHandler> _logger;

//    public ChatMessageCreatedEventHandler(ILogger<ChatMessageCreatedEventHandler> logger) => _logger = logger;

//    public override Task Handle(EntityCreatedEvent<ChatMessage> @event, CancellationToken cancellationToken)
//    {
//        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
//        return Task.CompletedTask;
//    }
//}