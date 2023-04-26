using FSH.WebApi.Domain.Price;

namespace FSH.WebApi.Application.Price.PriceGroups;

public class CreatePriceGroupRequest : IRequest<DefaultIdType>
{
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool? IsActive { get; set; }
}

public class CreatePriceGroupRequestHandler : IRequestHandler<CreatePriceGroupRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<PriceGroup> _repository;

    public CreatePriceGroupRequestHandler(IRepositoryWithEvents<PriceGroup> repository) => _repository = repository;

    public async Task<DefaultIdType> Handle(CreatePriceGroupRequest request, CancellationToken cancellationToken)
    {
        var entity = new PriceGroup(
                request.Order,
                request.Code,
                request.Name,
                request.Description ?? string.Empty,
                request.IsActive ?? true);

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}
