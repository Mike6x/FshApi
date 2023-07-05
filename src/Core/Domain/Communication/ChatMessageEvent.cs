namespace FSH.WebApi.Domain.Communication;

public abstract class ChatMessageEvent : DomainEvent
{
    public DefaultIdType Id { get; set; } = default!;

    protected ChatMessageEvent(DefaultIdType id)
    {
        Id = id;
    }
}

public class ChatMessageCreatedEvent : ChatMessageEvent
{
    public ChatMessageCreatedEvent(DefaultIdType id)
        : base(id)
    {
    }
}