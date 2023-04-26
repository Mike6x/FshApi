using FSH.WebApi.Domain.Place;

namespace FSH.WebApi.Application.Place.Channels;

public class UpdateChannelRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;

    public bool IsActive { get; set; }
}

public class UpdateChannelRequestHandler : IRequestHandler<UpdateChannelRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Channel> _repository;
    private readonly IStringLocalizer _t;

    public UpdateChannelRequestHandler(IRepositoryWithEvents<Channel> repository, IStringLocalizer<UpdateChannelRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateChannelRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);

        entity.Update(
            request.Order,
            request.Code,
            request.Name,
            request.Description,
            request.IsActive);

        await _repository.UpdateAsync(entity, cancellationToken);

        return request.Id;
    }
}