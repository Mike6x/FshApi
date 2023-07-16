using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.CronJobs;

public class CreateCronJobRequest : IRequest<DefaultIdType>
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

    public DateTime RunTime { get; set; }

    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }

    public int TotalRecord { get; set; }
    public int NumberOfSuccessed { get; set; }
    public int NumberOfFailed { get; set; }
    public int NumberOfDuplicated { get; set; }
    public int NumberOfExisted { get; set; }
}

public class CreateCronJobRequestHandler(IRepositoryWithEvents<CronJob> repository)
    : IRequestHandler<CreateCronJobRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<CronJob> _repository = repository;

    public async Task<DefaultIdType> Handle(CreateCronJobRequest request, CancellationToken cancellationToken)
    {
        var entity = new CronJob(
            request.Code,
            request.Name,
            request.Description,
            request.RunTime,
            request.FromDate,
            request.ToDate,
            request.TotalRecord,
            request.NumberOfSuccessed,
            request.NumberOfDuplicated,
            request.NumberOfExisted,
            request.NumberOfFailed);

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}
