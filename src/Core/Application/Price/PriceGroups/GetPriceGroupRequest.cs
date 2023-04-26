using FSH.WebApi.Domain.Price;

namespace FSH.WebApi.Application.Price.PriceGroups;

public class GetPriceGroupRequest : IRequest<PriceGroupDetailsDto>
{
    public Guid Id { get; set; }
    public GetPriceGroupRequest(Guid id) => Id = id;
}

public class GetPriceGroupRequestHandler : IRequestHandler<GetPriceGroupRequest, PriceGroupDetailsDto>
{
    private readonly IRepository<PriceGroup> _repository;
    private readonly IStringLocalizer _t;

    public GetPriceGroupRequestHandler(IRepository<PriceGroup> repository, IStringLocalizer<GetPriceGroupRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<PriceGroupDetailsDto> Handle(GetPriceGroupRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<PriceGroup, PriceGroupDetailsDto>)new PriceGroupByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);
}