using FSH.WebApi.Application.Common.Persistence;
using MediatR;

namespace FSH.WebApi.Infrastructure.Chat;
public class CreateChatMessageRequest : IRequest<DefaultIdType>
{
    public string FromUserId { get; set; } = default!;
    public string ToUserId { get; set; } = default!;
    public string Message { get; set; } = default!;
    public bool? IsRead { get; set; }
}

public class CreateChatMessageRequestHandler : IRequestHandler<CreateChatMessageRequest, DefaultIdType>
{
    private readonly IRepository<ChatMessage> _repository;

    public CreateChatMessageRequestHandler(IRepository<ChatMessage> repository) => _repository = repository;

    public async Task<DefaultIdType> Handle(CreateChatMessageRequest request, CancellationToken cancellationToken)
    {
        var entity = new ChatMessage(
                request.FromUserId,
                request.ToUserId,
                request.Message,
                request.IsRead ?? false);

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}