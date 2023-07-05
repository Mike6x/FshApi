using Ardalis.Specification;
using FSH.WebApi.Application.Common.Models;
using FSH.WebApi.Application.Common.Persistence;
using FSH.WebApi.Application.Common.Specification;
using MediatR;

namespace FSH.WebApi.Infrastructure.Chat;

public class SearchChatMessagesRequest : PaginationFilter, IRequest<PaginationResponse<ChatMessageDto>>
{
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
                .OrderBy(e => e.CreatedOn, !request.HasOrderBy());
}
