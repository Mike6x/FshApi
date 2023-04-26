using FSH.WebApi.Domain.Place;

namespace FSH.WebApi.Application.Place.Channels;

public class GetChannelRequest : IRequest<ChannelDetailsDto>
{
    public Guid Id { get; set; }
    public GetChannelRequest(Guid id) => Id = id;
}

public class GetChannelRequestHandler : IRequestHandler<GetChannelRequest, ChannelDetailsDto>
{
    private readonly IRepository<Channel> _repository;
    private readonly IStringLocalizer _t;

    public GetChannelRequestHandler(IRepository<Channel> repository, IStringLocalizer<GetChannelRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<ChannelDetailsDto> Handle(GetChannelRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<Channel, ChannelDetailsDto>)new ChannelByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);
}