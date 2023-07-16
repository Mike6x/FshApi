using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.EntityCodes;

public class CreateEntityCodeRequest : IRequest<DefaultIdType>
{
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string Seperator { get; set; } = "-";
    public int? Value { get; set; }
    public CodeType? Type { get; set; }
    public bool? IsActive { get; set; }
}

public class CreateEntityCodeRequestHandler(IRepositoryWithEvents<EntityCode> repository)
    : IRequestHandler<CreateEntityCodeRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<EntityCode> _repository = repository;

    public async Task<DefaultIdType> Handle(CreateEntityCodeRequest request, CancellationToken cancellationToken)
    {
        var entity = new EntityCode(
            request.Order,
            request.Code,
            request.Name,
            request.Description,
            request.Seperator,
            request.Value ?? 0,
            request.Type ?? CodeType.MasterData,
            request.IsActive);

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}
