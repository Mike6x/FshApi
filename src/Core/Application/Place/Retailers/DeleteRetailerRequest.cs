using FSH.WebApi.Application.Place.Stores;
using FSH.WebApi.Domain.Place;

namespace FSH.WebApi.Application.Place.Retailers;

public class DeleteRetailerRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteRetailerRequest(Guid id) => Id = id;
}

public class DeleteRetailerRequestHandler : IRequestHandler<DeleteRetailerRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Retailer> _repository;
    private readonly IReadRepository<Store> _childRepository;
    private readonly IStringLocalizer _t;

    public DeleteRetailerRequestHandler(IRepositoryWithEvents<Retailer> repository, IReadRepository<Store> childRepository, IStringLocalizer<DeleteRetailerRequestHandler> localizer) =>
        (_repository, _childRepository, _t) = (repository, childRepository, localizer);

    public async Task<Guid> Handle(DeleteRetailerRequest request, CancellationToken cancellationToken)
    {
        if (await _childRepository.AnyAsync(new StoresByRetailerSpec(request.Id), cancellationToken))
        {
            throw new ConflictException(_t["Retailer cannot be deleted as it's being used."]);
        }

        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity ?? throw new NotFoundException(_t["Retailer {0} Not Found."]);

        await _repository.DeleteAsync(entity, cancellationToken);

        return request.Id;
    }
}