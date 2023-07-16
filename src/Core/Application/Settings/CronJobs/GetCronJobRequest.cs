using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.CronJobs;

public class GetCronJobRequest(DefaultIdType id) : IRequest<CronJobDetailsDto>
{
    public DefaultIdType Id { get; set; } = id;
}

public class GetCronJobRequestHandler : IRequestHandler<GetCronJobRequest, CronJobDetailsDto>
{
    private readonly IRepository<CronJob> _repository;
    private readonly IStringLocalizer _t;

    public GetCronJobRequestHandler(IRepository<CronJob> repository, IStringLocalizer<GetCronJobRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<CronJobDetailsDto> Handle(GetCronJobRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<CronJob, CronJobDetailsDto>)new CronJobByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);
}