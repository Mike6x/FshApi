using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.Dimensions;

public class UpdateDimensionRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }

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
    public DefaultIdType FatherId { get; set; }
}

public class UpdateDimensionRequestHandler : IRequestHandler<UpdateDimensionRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Dimension> _repository;
    private readonly IStringLocalizer _t;

    public UpdateDimensionRequestHandler(IRepositoryWithEvents<Dimension> repository, IStringLocalizer<UpdateDimensionRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<DefaultIdType> Handle(UpdateDimensionRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);

        entity.Update(
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

        await _repository.UpdateAsync(entity, cancellationToken);

        return request.Id;
    }
}