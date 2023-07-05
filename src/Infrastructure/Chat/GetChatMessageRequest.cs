using Ardalis.Specification;
using FSH.WebApi.Application.Common.Exceptions;
using FSH.WebApi.Application.Common.Persistence;
using MediatR;
using Microsoft.Extensions.Localization;

namespace FSH.WebApi.Infrastructure.Chat;
public class GetChatMessageRequest : IRequest<ChatMessageDetailsDto>
{
    public DefaultIdType Id { get; set; }
    public GetChatMessageRequest(DefaultIdType id) => Id = id;
}

public class GetChatMessageRequestHandler : IRequestHandler<GetChatMessageRequest, ChatMessageDetailsDto>
{
    private readonly IRepository<ChatMessage> _repository;
    private readonly IStringLocalizer _t;

    public GetChatMessageRequestHandler(IRepository<ChatMessage> repository, IStringLocalizer<GetChatMessageRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<ChatMessageDetailsDto> Handle(GetChatMessageRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<ChatMessage, ChatMessageDetailsDto>)new ChatMessageByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);
}