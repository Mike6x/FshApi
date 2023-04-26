using FSH.WebApi.Domain.Property;

namespace FSH.WebApi.Application.Property.AssetStatuses;

public class CreateAssetStatusRequest : IRequest<DefaultIdType>
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

    public AssetStatusType Type { get; set; }
}

public class CreateAssetStatusRequestHandler : IRequestHandler<CreateAssetStatusRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<AssetStatus> _repository;

    public CreateAssetStatusRequestHandler(IRepositoryWithEvents<AssetStatus> repository) => _repository = repository;

    public async Task<DefaultIdType> Handle(CreateAssetStatusRequest request, CancellationToken cancellationToken)
    {
        var entity = new AssetStatus(
                request.Code,
                request.Name,
                request.Description ?? string.Empty,
                request.Type);

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}
