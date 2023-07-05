using Ardalis.Specification;
using FSH.WebApi.Application.Common.Models;
using FSH.WebApi.Application.Common.Persistence;
using FSH.WebApi.Application.Common.Specification;
using MediatR;

namespace FSH.WebApi.Infrastructure.Chat;

public class GetChatConversationRequest : BaseFilter, IRequest<List<ChatMessageDto>>
{
    public string UserId { get; set; }
    public string ContactId { get; set; }
    public GetChatConversationRequest(string userId, string contactId)
    {
        UserId = userId;
        ContactId = contactId;
    }
}

public class GetChatConversationRequestHandler : IRequestHandler<GetChatConversationRequest, List<ChatMessageDto>>
{
    private readonly IRepository<ChatMessage> _repository;
    public GetChatConversationRequestHandler(IRepository<ChatMessage> repository)
        => _repository = repository;

    public async Task<List<ChatMessageDto>> Handle(GetChatConversationRequest request, CancellationToken cancellationToken)
    {
        var spec = new GetChatConversationSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);
        return list ?? new List<ChatMessageDto>();
    }
}

public class GetChatConversationSpecification : EntitiesByBaseFilterSpec<ChatMessage, ChatMessageDto>
{
    public GetChatConversationSpecification(GetChatConversationRequest request)
        : base(request) =>
            Query
                .OrderBy(e => e.CreatedDate)
                .Where(e =>
                    (e.FromUserId == request.UserId && e.ToUserId == request.ContactId) ||
                    (e.FromUserId == request.ContactId && e.ToUserId == request.UserId));
}
