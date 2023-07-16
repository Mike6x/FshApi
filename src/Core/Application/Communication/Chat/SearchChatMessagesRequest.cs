using FSH.WebApi.Domain.Communication;
using MediatR;

namespace FSH.WebApi.Application.Communication.Chat;

public class SearchChatMessagesRequest : PaginationFilter, IRequest<PaginationResponse<ChatMessageDto>>
{
    public string? FromUserId { get; set; }
    public string? ToUserId { get; set; }
    public bool? IsRead { get; set; }
}

public class SearchChatMessagesRequestHandler : IRequestHandler<SearchChatMessagesRequest, PaginationResponse<ChatMessageDto>>
{
    private readonly IRepository<ChatMessage> _repository;

    public SearchChatMessagesRequestHandler(IRepository<ChatMessage> repository) => _repository = repository;

    public async Task<PaginationResponse<ChatMessageDto>> Handle(SearchChatMessagesRequest request, CancellationToken cancellationToken)
    {
        var spec = new SearchChatMessagesSpecification(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}

public class SearchChatMessagesSpecification : EntitiesByPaginationFilterSpec<ChatMessage, ChatMessageDto>
{
    public SearchChatMessagesSpecification(SearchChatMessagesRequest request)
        : base(request) =>
            Query
                .OrderBy(e => e.CreatedOn, !request.HasOrderBy())
                    .Where(e => e.IsRead.Equals(request.IsRead!), request.IsRead.HasValue)
                    .Where(e => e.FromUserId.Equals(request.FromUserId), !string.IsNullOrEmpty(request.FromUserId))
                    .Where(e => e.ToUserId.Equals(request.ToUserId), !string.IsNullOrEmpty(request.ToUserId));
}
