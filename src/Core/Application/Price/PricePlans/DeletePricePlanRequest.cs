using FSH.WebApi.Domain.Price;

namespace FSH.WebApi.Application.Price.PricePlans;

public class DeletePricePlanRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeletePricePlanRequest(Guid id) => Id = id;
}

public class DeletePricePlanRequestHandler : IRequestHandler<DeletePricePlanRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<PricePlan> _repository;
    private readonly IStringLocalizer _t;

    public DeletePricePlanRequestHandler(IRepositoryWithEvents<PricePlan> repository, IStringLocalizer<DeletePricePlanRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(DeletePricePlanRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity ?? throw new NotFoundException(_t["PricePlan {0} Not Found."]);

        await _repository.DeleteAsync(entity, cancellationToken);

        return request.Id;
    }
}
