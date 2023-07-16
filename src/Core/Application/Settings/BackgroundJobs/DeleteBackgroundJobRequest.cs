using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.BackgroundJobs;

public class DeleteBackgroundJobRequest(DefaultIdType id) : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; } = id;
}

public class DeleteBackgroundJobRequestHandler : IRequestHandler<DeleteBackgroundJobRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<BackgroundJob> _repository;
    private readonly IStringLocalizer _t;
    private readonly IJobService _jobService;
    public DeleteBackgroundJobRequestHandler(IRepositoryWithEvents<BackgroundJob> repository, IJobService jobService, IStringLocalizer<DeleteBackgroundJobRequestHandler> localizer)
        => (_repository, _jobService, _t) = (repository, jobService, localizer);

    public async Task<DefaultIdType> Handle(DeleteBackgroundJobRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity ?? throw new NotFoundException(_t["BackgroundJob {0} Not Found.", entity.Code]);

        await _repository.DeleteAsync(entity, cancellationToken);

        bool result = entity.Type switch
        {
            BackgroundJobType.Hourly or
            BackgroundJobType.Daily or
            BackgroundJobType.Monthy => _jobService.DeleteRecurring(entity.Code),
            _ => _jobService.Delete(entity.Code),
        };

        if (!result) throw new NotFoundException(_t["HangFireJob {0} Not Found.", entity.Code]);

        return request.Id;
    }
}
