using FSH.WebApi.Application.Price.PricePlans;
using FSH.WebApi.Domain.Price;

namespace FSH.WebApi.Application.Price.PriceGroups;

public class DeletePriceGroupRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeletePriceGroupRequest(Guid id) => Id = id;
}

public class DeletePriceGroupRequestHandler : IRequestHandler<DeletePriceGroupRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<PriceGroup> _repository;
    private readonly IReadRepository<PricePlan> _childRepository;
    private readonly IStringLocalizer _t;

    public DeletePriceGroupRequestHandler(IRepositoryWithEvents<PriceGroup> repository, IReadRepository<PricePlan> childRepository, IStringLocalizer<DeletePriceGroupRequestHandler> localizer) =>
        (_repository, _childRepository, _t) = (repository, childRepository, localizer);

    public async Task<Guid> Handle(DeletePriceGroupRequest request, CancellationToken cancellationToken)
    {
        if (await _childRepository.AnyAsync(new PricePlansByPriceGroupSpec(request.Id), cancellationToken))
        {
            throw new ConflictException(_t["PriceGroup cannot be deleted as it's being used."]);
        }

        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity ?? throw new NotFoundException(_t["PriceGroup {0} Not Found."]);

        await _repository.DeleteAsync(entity, cancellationToken);

        return request.Id;
    }
}
