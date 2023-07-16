using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.CronJobs;

public class UpdateCronJobRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    public DateTime? RunTime { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }

    public int? TotalRecord { get; set; }
    public int? NumberOfSuccessed { get; set; }
    public int? NumberOfFailed { get; set; }
    public int? NumberOfDuplicated { get; set; }
    public int? NumberOfExisted { get; set; }
}

public class UpdateApiCronJobRequestHandler : IRequestHandler<UpdateCronJobRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<CronJob> _repository;
    private readonly IStringLocalizer _t;

    public UpdateApiCronJobRequestHandler(IRepositoryWithEvents<CronJob> repository, IStringLocalizer<UpdateApiCronJobRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<DefaultIdType> Handle(UpdateCronJobRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);

        entity.Update(
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

        await _repository.UpdateAsync(entity, cancellationToken);

        return request.Id;
    }
}