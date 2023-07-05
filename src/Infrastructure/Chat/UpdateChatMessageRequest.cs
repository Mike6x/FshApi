using FSH.WebApi.Application.Common.Exceptions;
using FSH.WebApi.Application.Common.Persistence;
using MediatR;
using Microsoft.Extensions.Localization;

namespace FSH.WebApi.Infrastructure.Chat;

public class UpdateChatMessageRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }

    public string FromUserId { get; set; } = default!;
    public string ToUserId { get; set; } = default!;
    public string? Message { get; set; }
    public DateTime? CreatedDate { get; set; }
    public bool? IsRead { get; set; }
}

public class UpdateChatMessageRequestHandler : IRequestHandler<UpdateChatMessageRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepository<ChatMessage> _repository;
    private readonly IStringLocalizer _t;

    public UpdateChatMessageRequestHandler(IRepository<ChatMessage> repository, IStringLocalizer<UpdateChatMessageRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<DefaultIdType> Handle(UpdateChatMessageRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);

        entity.Update(
                request.FromUserId,
                request.ToUserId,
                request.Message,
                request.CreatedDate,
                request.IsRead);

        await _repository.UpdateAsync(entity, cancellationToken);

        return request.Id;
    }
}