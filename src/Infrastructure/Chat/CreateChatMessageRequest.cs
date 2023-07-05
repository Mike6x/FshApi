using FSH.WebApi.Application.Common.Events;
using FSH.WebApi.Application.Common.Persistence;
using FSH.WebApi.Domain.Communication;
using MediatR;

namespace FSH.WebApi.Infrastructure.Chat;

public class CreateChatMessageRequest : IRequest<DefaultIdType>
{
    public string FromUserId { get; set; } = default!;
    public string ToUserId { get; set; } = default!;
    public string Message { get; set; } = default!;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public bool? IsRead { get; set; }
}

public class CreateChatMessageRequestHandler : IRequestHandler<CreateChatMessageRequest, DefaultIdType>
{
    private readonly IRepository<ChatMessage> _repository;
    private readonly IEventPublisher _events;
    public CreateChatMessageRequestHandler(IRepository<ChatMessage> repository, IEventPublisher events)
    {
        _repository = repository;
        _events = events;
    }

    public async Task<DefaultIdType> Handle(CreateChatMessageRequest request, CancellationToken cancellationToken)
    {
        var entity = new ChatMessage(
                request.FromUserId,
                request.ToUserId,
                request.Message,
                request.CreatedDate,
                request.IsRead ?? false);

        await _repository.AddAsync(entity, cancellationToken);

        // Add Domain Events to be raised after the commit
        // entity.DomainEvents.Add(EntityCreatedEvent.WithEntity(ChatMessage));

        await _events.PublishAsync(new ChatMessageCreatedEvent(entity.Id));

        return entity.Id;
    }
}