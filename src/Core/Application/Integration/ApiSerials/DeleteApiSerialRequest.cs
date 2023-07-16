using FSH.WebApi.Domain.Integration;

namespace FSH.WebApi.Application.Integration.ApiSerials;

public class DeleteApiSerialRequest(DefaultIdType id) : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; } = id;
}

public class DeleteApiSerialRequestHandler : IRequestHandler<DeleteApiSerialRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<ApiSerial> _repository;
    private readonly IStringLocalizer _t;

    public DeleteApiSerialRequestHandler(IRepositoryWithEvents<ApiSerial> repository, IStringLocalizer<DeleteApiSerialRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<DefaultIdType> Handle(DeleteApiSerialRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity ?? throw new NotFoundException(_t["ApiSerial {0} Not Found.", request.Id]);

        await _repository.DeleteAsync(entity, cancellationToken);

        return request.Id;
    }
}
