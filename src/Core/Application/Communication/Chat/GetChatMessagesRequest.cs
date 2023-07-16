using FSH.WebApi.Domain.Communication;

namespace FSH.WebApi.Application.Communication.Chat;
public class GetChatMessagesRequest : BaseFilter, IRequest<List<ChatMessageDto>>
{
    public string? FromUserId { get; set; }
    public string? ToUserId { get; set; }
    public bool? IsRead { get; set; }
}

public class GetChatMessagesRequestHandler : IRequestHandler<GetChatMessagesRequest, List<ChatMessageDto>>
{
    private readonly IRepository<ChatMessage> _repository;

    public GetChatMessagesRequestHandler(IRepository<ChatMessage> repository) => _repository = repository;

    public async Task<List<ChatMessageDto>> Handle(GetChatMessagesRequest request, CancellationToken cancellationToken)
    {
        var spec = new GetChatMessagesSpecification(request);
        return await _repository.ListAsync(spec, cancellationToken);
    }
}

public class GetChatMessagesSpecification : EntitiesByBaseFilterSpec<ChatMessage, ChatMessageDto>
{
    public GetChatMessagesSpecification(GetChatMessagesRequest request)
        : base(request) =>
            Query
                .OrderBy(e => e.CreatedDate)
                    .Where(e => e.IsRead.Equals(request.IsRead!), request.IsRead.HasValue)
                    .Where(e => e.FromUserId == request.FromUserId || e.ToUserId == request.FromUserId, !string.IsNullOrEmpty(request.FromUserId))
                    .Where(e => e.FromUserId == request.ToUserId || e.ToUserId == request.ToUserId, !string.IsNullOrEmpty(request.ToUserId));
}