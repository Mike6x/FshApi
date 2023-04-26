namespace FSH.WebApi.Application.Catalog.BusinessLines;

public class CreateBusinessLineRequest : IRequest<DefaultIdType>
{
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool? IsActive { get; set; }
}

public class CreateBusinessLineRequestHandler : IRequestHandler<CreateBusinessLineRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<BusinessLine> _repository;

    public CreateBusinessLineRequestHandler(IRepositoryWithEvents<BusinessLine> repository) => _repository = repository;

    public async Task<DefaultIdType> Handle(CreateBusinessLineRequest request, CancellationToken cancellationToken)
    {
        var entity = new BusinessLine(
            request.Order,
            request.Code,
            request.Name,
            request.Description ?? string.Empty,
            request.IsActive ?? true);

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}
