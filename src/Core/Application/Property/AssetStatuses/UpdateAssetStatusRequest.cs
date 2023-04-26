using FSH.WebApi.Domain.Property;

namespace FSH.WebApi.Application.Property.AssetStatuses;

public class UpdateAssetStatusRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

    public AssetStatusType Type { get; set; }
}

public class UpdateAssetStatusRequestHandler : IRequestHandler<UpdateAssetStatusRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<AssetStatus> _repository;
    private readonly IStringLocalizer _t;

    public UpdateAssetStatusRequestHandler(IRepositoryWithEvents<AssetStatus> repository, IStringLocalizer<UpdateAssetStatusRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<DefaultIdType> Handle(UpdateAssetStatusRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);

        entity.Update(
            request.Code,
            request.Name,
            request.Description,
            request.Type);

        await _repository.UpdateAsync(entity, cancellationToken);

        return request.Id;
    }
}