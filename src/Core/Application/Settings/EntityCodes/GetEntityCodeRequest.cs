using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.EntityCodes;

public class GetEntityCodeRequest(DefaultIdType id) : IRequest<EntityCodeDetailsDto>
{
    public DefaultIdType Id { get; set; } = id;
}

public class GetEntityCodeRequestHandler : IRequestHandler<GetEntityCodeRequest, EntityCodeDetailsDto>
{
    private readonly IRepository<EntityCode> _repository;
    private readonly IStringLocalizer _t;

    public GetEntityCodeRequestHandler(IRepository<EntityCode> repository, IStringLocalizer<GetEntityCodeRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<EntityCodeDetailsDto> Handle(GetEntityCodeRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<EntityCode, EntityCodeDetailsDto>)new EntityCodeByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);
}