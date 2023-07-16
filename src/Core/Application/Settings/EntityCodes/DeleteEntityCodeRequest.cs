using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.EntityCodes;

public class DeleteEntityCodeRequest(DefaultIdType id) : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; } = id;
}

public class DeleteEntityCodeRequestHandler : IRequestHandler<DeleteEntityCodeRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<EntityCode> _repository;
    private readonly IStringLocalizer _t;

    public DeleteEntityCodeRequestHandler(IRepositoryWithEvents<EntityCode> repository, IStringLocalizer<DeleteEntityCodeRequestHandler> localizer) =>
        (_repository,  _t) = (repository,  localizer);

    public async Task<DefaultIdType> Handle(DeleteEntityCodeRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity ?? throw new NotFoundException(_t["EntityCode {0} Not Found.", request.Id]);

        await _repository.DeleteAsync(entity, cancellationToken);

        return request.Id;
    }
}
