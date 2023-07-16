namespace FSH.WebApi.Domain.Communication;

public abstract class ChatMessageEvent : DomainEvent
{
    public DefaultIdType Id { get; set; } = default!;

    public string ReceiverUserId { get; set; } = default!;
    public string SenderUserId { get; set; } = default!;

    protected ChatMessageEvent(DefaultIdType id, string receiverUserId, string senderUserId)
    {
        Id = id;
        ReceiverUserId = receiverUserId;
        SenderUserId = senderUserId;
    }
}

public class ChatMessageCreatedEvent : ChatMessageEvent
{
    public ChatMessageCreatedEvent(DefaultIdType id, string receiverUserId, string senderUserId)
        : base(id, receiverUserId, senderUserId)
    {
    }
}