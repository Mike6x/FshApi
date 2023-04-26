using FSH.WebApi.Domain.Place;

namespace FSH.WebApi.Application.Place.Stores;

public class DeleteStoreRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteStoreRequest(Guid id) => Id = id;
}

public class DeleteStoreRequestHandler : IRequestHandler<DeleteStoreRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Store> _repository;
    private readonly IReadRepository<Stock> _childRepository;
    private readonly IStringLocalizer _t;

    public DeleteStoreRequestHandler(IRepositoryWithEvents<Store> repository, IReadRepository<Stock> childRepository, IStringLocalizer<DeleteStoreRequestHandler> localizer) =>
        (_repository, _childRepository, _t) = (repository, childRepository, localizer);

    public async Task<Guid> Handle(DeleteStoreRequest request, CancellationToken cancellationToken)
    {
        // if (await _childRepository.AnyAsync(new StoresByStockSpec(request.Id), cancellationToken))
        // {
        //    throw new ConflictException(_t["Store cannot be deleted as it's being used."]);
        // }

        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity ?? throw new NotFoundException(_t["Store {0} Not Found."]);

        await _repository.DeleteAsync(entity, cancellationToken);

        return request.Id;
    }
}