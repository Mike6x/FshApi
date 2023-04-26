using FSH.WebApi.Domain.Organization;

namespace FSH.WebApi.Application.Organization.BusinessUnits;

public class CreateBusinessUnitRequest : IRequest<DefaultIdType>
{
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool? IsActive { get; set; }
}

public class CreateBusinessUnitRequestHandler : IRequestHandler<CreateBusinessUnitRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<BusinessUnit> _repository;

    public CreateBusinessUnitRequestHandler(IRepositoryWithEvents<BusinessUnit> repository) => _repository = repository;

    public async Task<DefaultIdType> Handle(CreateBusinessUnitRequest request, CancellationToken cancellationToken)
    {
        var entity = new BusinessUnit(
                request.Order,
                request.Code,
                request.Name,
                request.Description ?? string.Empty,
                request.IsActive ?? true);

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}
