using FSH.WebApi.Domain.Place;

namespace FSH.WebApi.Application.Place.Retailers;

public class GetRetailerRequest : IRequest<RetailerDetailsDto>
{
    public Guid Id { get; set; }
    public GetRetailerRequest(Guid id) => Id = id;
}

public class GetRetailerRequestHandler : IRequestHandler<GetRetailerRequest, RetailerDetailsDto>
{
    private readonly IRepository<Retailer> _repository;
    private readonly IStringLocalizer _t;

    public GetRetailerRequestHandler(IRepository<Retailer> repository, IStringLocalizer<GetRetailerRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<RetailerDetailsDto> Handle(GetRetailerRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<Retailer, RetailerDetailsDto>)new RetailerByIdWithChannelSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);
}