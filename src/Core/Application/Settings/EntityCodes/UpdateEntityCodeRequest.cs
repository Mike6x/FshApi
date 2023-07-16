using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.EntityCodes;

public class UpdateEntityCodeRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }
    public int? Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Seperator { get; set; } = "-";
    public int? Value { get; set; }
    public CodeType Type { get; set; }

    public bool? IsActive { get; set; }
}

public class UpdateEntityCodeRequestHandler : IRequestHandler<UpdateEntityCodeRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<EntityCode> _repository;
    private readonly IStringLocalizer _t;

    public UpdateEntityCodeRequestHandler(IRepositoryWithEvents<EntityCode> repository, IStringLocalizer<UpdateEntityCodeRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<DefaultIdType> Handle(UpdateEntityCodeRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);

        entity.Update(
            request.Order,
            request.Code,
            request.Name,
            request.Description,
            request.Seperator,
            request.Value,
            request.Type,
            request.IsActive);

        await _repository.UpdateAsync(entity, cancellationToken);

        return request.Id;
    }
}