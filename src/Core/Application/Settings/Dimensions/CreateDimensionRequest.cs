using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.Dimensions;
public class CreateDimensionRequest : IRequest<DefaultIdType>
{
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }

    public string? FullName { get; set; }
    public string? NativeName { get; set; }
    public string? FullNativeName { get; set; }
    public int Value { get; set; }
    public string Type { get; set; } = default!;
    public Guid? FatherId { get; set; }
}

public class CreateDimensionRequestHandler(IRepositoryWithEvents<Dimension> repository)
    : IRequestHandler<CreateDimensionRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Dimension> _repository = repository;

    public async Task<DefaultIdType> Handle(CreateDimensionRequest request, CancellationToken cancellationToken)
    {
        var entity = new Dimension(
            request.Order,
            request.Code,
            request.Name,
            request.Description,
            request.IsActive,
            request.FullName,
            request.NativeName,
            request.FullNativeName,
            request.Value,
            request.Type,
            request.FatherId);

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}
