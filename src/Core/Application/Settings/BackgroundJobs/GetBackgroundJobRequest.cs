using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.BackgroundJobs;

public class GetBackgroundJobRequest(DefaultIdType id) : IRequest<BackgroundJobDetailsDto>
{
    public DefaultIdType Id { get; set; } = id;
}

public class GetBackgroundJobRequestHandler : IRequestHandler<GetBackgroundJobRequest, BackgroundJobDetailsDto>
{
    private readonly IRepository<BackgroundJob> _repository;
    private readonly IStringLocalizer _t;

    public GetBackgroundJobRequestHandler(IRepository<BackgroundJob> repository, IStringLocalizer<GetBackgroundJobRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<BackgroundJobDetailsDto> Handle(GetBackgroundJobRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<BackgroundJob, BackgroundJobDetailsDto>)new BackgroundJobByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);
}