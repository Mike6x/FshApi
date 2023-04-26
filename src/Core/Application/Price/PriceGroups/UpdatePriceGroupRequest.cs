using FSH.WebApi.Domain.Price;

namespace FSH.WebApi.Application.Price.PriceGroups;

public class UpdatePriceGroupRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;

    public bool IsActive { get; set; }
}

public class UpdatePriceGroupRequestHandler : IRequestHandler<UpdatePriceGroupRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<PriceGroup> _repository;
    private readonly IStringLocalizer _t;

    public UpdatePriceGroupRequestHandler(IRepositoryWithEvents<PriceGroup> repository, IStringLocalizer<UpdatePriceGroupRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdatePriceGroupRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);

        entity.Update(
            request.Order,
            request.Code,
            request.Name,
            request.Description,
            request.IsActive);

        await _repository.UpdateAsync(entity, cancellationToken);

        return request.Id;
    }
}