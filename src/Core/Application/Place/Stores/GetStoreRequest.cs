using FSH.WebApi.Domain.Place;

namespace FSH.WebApi.Application.Place.Stores;

public class GetStoreRequest : IRequest<StoreDetailsDto>
{
    public Guid Id { get; set; }
    public GetStoreRequest(Guid id) => Id = id;
}

public class GetStoreRequestHandler : IRequestHandler<GetStoreRequest, StoreDetailsDto>
{
    private readonly IRepository<Store> _repository;
    private readonly IStringLocalizer _t;

    public GetStoreRequestHandler(IRepository<Store> repository, IStringLocalizer<GetStoreRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<StoreDetailsDto> Handle(GetStoreRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<Store, StoreDetailsDto>)new StoreByIdWithRetailerSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);
}