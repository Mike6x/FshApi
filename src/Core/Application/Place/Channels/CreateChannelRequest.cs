using FSH.WebApi.Domain.Place;

namespace FSH.WebApi.Application.Place.Channels;

public class CreateChannelRequest : IRequest<DefaultIdType>
{
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool? IsActive { get; set; }
}

public class CreateChannelRequestHandler : IRequestHandler<CreateChannelRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Channel> _repository;

    public CreateChannelRequestHandler(IRepositoryWithEvents<Channel> repository) => _repository = repository;

    public async Task<DefaultIdType> Handle(CreateChannelRequest request, CancellationToken cancellationToken)
    {
        var entity = new Channel(
                request.Order,
                request.Code,
                request.Name,
                request.Description ?? string.Empty,
                request.IsActive ?? true);

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}
