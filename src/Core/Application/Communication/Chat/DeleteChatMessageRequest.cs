using FSH.WebApi.Domain.Communication;

namespace FSH.WebApi.Application.Communication.Chat;
public class DeleteChatMessageRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }

    public DeleteChatMessageRequest(DefaultIdType id) => Id = id;
}

public class DeleteChatMessageRequestHandler : IRequestHandler<DeleteChatMessageRequest, DefaultIdType>
{
    private readonly IRepository<ChatMessage> _repository;
    private readonly IStringLocalizer _t;

    public DeleteChatMessageRequestHandler(IRepository<ChatMessage> repository, IStringLocalizer<DeleteChatMessageRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<DefaultIdType> Handle(DeleteChatMessageRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity ?? throw new NotFoundException(_t["ChatMessage {0} Not Found."]);

        await _repository.DeleteAsync(entity, cancellationToken);

        return request.Id;
    }
}
