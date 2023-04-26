using FSH.WebApi.Application.Place.Retailers;
using FSH.WebApi.Domain.Place;

namespace FSH.WebApi.Application.Place.Channels;

public class DeleteChannelRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteChannelRequest(Guid id) => Id = id;
}

public class DeleteChannelRequestHandler : IRequestHandler<DeleteChannelRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Channel> _repository;
    private readonly IReadRepository<Retailer> _childRepository;
    private readonly IStringLocalizer _t;

    public DeleteChannelRequestHandler(IRepositoryWithEvents<Channel> repository, IReadRepository<Retailer> childRepository, IStringLocalizer<DeleteChannelRequestHandler> localizer) =>
        (_repository, _childRepository, _t) = (repository, childRepository, localizer);

    public async Task<Guid> Handle(DeleteChannelRequest request, CancellationToken cancellationToken)
    {
        if (await _childRepository.AnyAsync(new RetailersByChannelSpec(request.Id), cancellationToken))
        {
            throw new ConflictException(_t["Channel cannot be deleted as it's being used."]);
        }

        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity ?? throw new NotFoundException(_t["Channel {0} Not Found."]);

        await _repository.DeleteAsync(entity, cancellationToken);

        return request.Id;
    }
}
