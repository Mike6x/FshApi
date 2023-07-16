using FSH.WebApi.Domain.Communication;

namespace FSH.WebApi.Application.Communication.Chat;
public class RemoveChatMessagesRequest : BaseFilter, IRequest<List<ChatMessage>>
{
    public string? UserId { get; set; }
}

public class RemoveChatMessagesRequestHandler : IRequestHandler<RemoveChatMessagesRequest, List<ChatMessage>>
{
    private readonly IRepository<ChatMessage> _repository;
    private readonly IDapperRepository _dapperRepo;
    private readonly IStringLocalizer _t;

    public RemoveChatMessagesRequestHandler(IRepository<ChatMessage> repository, IDapperRepository dapperRepo, IStringLocalizer<RemoveChatMessagesRequestHandler> localizer) =>
        (_repository, _dapperRepo, _t) = (repository, dapperRepo, localizer);

    public async Task<List<ChatMessage>> Handle(RemoveChatMessagesRequest request, CancellationToken cancellationToken)
    {
        var spec = new GetUserChatConversationSpecification(request);
        var list = await _repository.ListAsync(spec, cancellationToken);

        if (list == null)
        {
            return new List<ChatMessage>();
        }
        else
        {
            await _dapperRepo.DeleteRangeAsync(list, cancellationToken);
            return list;
        }
    }
}

public class GetUserChatConversationSpecification : EntitiesByBaseFilterSpec<ChatMessage, ChatMessage>
{
    public GetUserChatConversationSpecification(RemoveChatMessagesRequest request)
        : base(request) =>
            Query
                .Where(e => e.FromUserId.Equals(request.UserId) || e.ToUserId.Equals(request.UserId), !string.IsNullOrEmpty(request.UserId));
}