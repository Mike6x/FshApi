using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.CronJobs;

public class DeleteCronJobRequest(DefaultIdType id) : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; } = id;
}

public class DeleteCronJobRequestHandler : IRequestHandler<DeleteCronJobRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<CronJob> _repository;
    private readonly IStringLocalizer _t;

    public DeleteCronJobRequestHandler(IRepositoryWithEvents<CronJob> repository, IStringLocalizer<DeleteCronJobRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<DefaultIdType> Handle(DeleteCronJobRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity ?? throw new NotFoundException(_t["CronJob {0} Not Found.", request.Id]);

        await _repository.DeleteAsync(entity, cancellationToken);

        return request.Id;
    }
}
