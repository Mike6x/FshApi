using FSH.WebApi.Application.Catalog.Brands;
using FSH.WebApi.Application.DummyJobs;
using FSH.WebApi.Application.Integration;
using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.BackgroundJobs;

public class CreateBackgroundJobRequest : IRequest<DefaultIdType>
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public BackgroundJobType Type { get; set; }
    public CommandList Command { get; set; }
    public DateTime RunTime { get; set; }

    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public int? RepeatTimes { get; set; }

    public DefaultIdType? FatherId { get; set; }
}

public class CreateBackgroundJobRequestHandler(
    IRepositoryWithEvents<BackgroundJob> repository,
    IJobService jobService,
    IStringLocalizer<UpdateBackgroundJobRequestHandler> localizer) : IRequestHandler<CreateBackgroundJobRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<BackgroundJob> _repository = repository;
    private readonly IStringLocalizer _t = localizer;
    private readonly IJobService _jobService = jobService;
    public async Task<DefaultIdType> Handle(CreateBackgroundJobRequest request, CancellationToken cancellationToken)
    {
        request.Code = JobRegister(request.Command, request.RunTime, request.FromDate, request.ToDate, request.RepeatTimes, request.Type);

        if (request.Code == Guid.Empty.ToString())
            throw new NotFoundException(_t["The Command: {0}, with type: {1}, is not Support.", request.Command.ToString(), request.Type]);

        var entity = new BackgroundJob(
                request.Code,
                nameof(CommandList.GetSerialsFromErp) + "_" + request.RunTime.ToString("yyyyMMdd_HHmm"),
                request.Description,
                request.Type,
                request.Command,
                request.RunTime,
                request.FromDate,
                request.ToDate,
                request.RepeatTimes,
                request.FatherId);

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }

    /// <summary>
    /// return JobId or Guid.Empty if can not registered.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="runTime"></param>
    /// <param name="fromDate"></param>
    /// <param name="toDate"></param>
    /// <param name="repeatTimes"></param>
    /// <param name="jobType"></param>
    /// <returns></returns>
    private string JobRegister(CommandList command, DateTime runTime, DateTime? fromDate, DateTime? toDate, int? repeatTimes, BackgroundJobType jobType)
    {
        var delayTime = (runTime > DateTime.UtcNow) ? runTime - DateTime.UtcNow : TimeSpan.Zero;
        var modify_from = fromDate ?? DateTime.Today;
        var modify_to = toDate ?? DateTime.Today.AddDays(1);
        if (modify_to <= modify_from) modify_to = modify_from.AddDays(1);

        var parameters = new Dictionary<string, string>
                        {
                            { "type", "serial" },
                            { "modify_to", modify_to.ToString("yyyy-MM-dd") },
                            { "modify_from", modify_from.ToString("yyyy-MM-dd") },
                            { "serial_num", string.Empty }
                        };

        return jobType switch
        {
            BackgroundJobType.Continuations or
            BackgroundJobType.FireAndForget => command switch
            {
                CommandList.GetSerialsFromErp =>
                    _jobService.Enqueue<IWarrantyApi>(x => x.GetAndUpdateSerialsAsync(parameters)),
                CommandList.BrandGenerator =>
                     _jobService.Enqueue<IBrandGeneratorJob>(x => x.GenerateAsync(repeatTimes ?? 1, default)),
                CommandList.DeleteRandomBrand =>
                    _jobService.Enqueue<IBrandGeneratorJob>(x => x.CleanAsync(default)),
                CommandList.SendEmail =>
                    _jobService.Enqueue<IDummyEmailService>(x => x.SendEmail("FireAndForget Sample Job", DateTime.UtcNow.ToString())),

                _ => Guid.Empty.ToString(),
            },
            BackgroundJobType.Delayed => command switch
            {
                CommandList.GetSerialsFromErp =>
                    _jobService.Schedule<IWarrantyApi>(x => x.GetAndUpdateSerialsAsync(parameters), delayTime),

                CommandList.BrandGenerator =>
                     _jobService.Schedule<IBrandGeneratorJob>(x => x.GenerateAsync(repeatTimes ?? 1, default), delayTime),
                CommandList.DeleteRandomBrand =>
                    _jobService.Schedule<IBrandGeneratorJob>(x => x.CleanAsync(default), delayTime),
                CommandList.SendEmail =>
                    _jobService.Schedule<IDummyEmailService>(
                        x => x.SendEmail("Delayed Sample Job", DateTime.UtcNow.ToString()), delayTime),

                _ => Guid.Empty.ToString(),
            },
            BackgroundJobType.Monthy or
            BackgroundJobType.Weekly or
            BackgroundJobType.Daily or
            BackgroundJobType.Hourly => command switch
            {
                CommandList.GetSerialsFromErp =>
                    _jobService.Recurring<IWarrantyApi>(
                        Guid.NewGuid().ToString(),
                        x => x.GetSerialsAsync(null),
                        jobType.ToString(),
                        runTime),
                CommandList.BrandGenerator =>
                    _jobService.Recurring<IBrandGeneratorJob>(
                        Guid.NewGuid().ToString(),
                        x => x.GenerateAsync(repeatTimes ?? 1, default),
                        jobType.ToString(),
                        runTime),
                CommandList.DeleteRandomBrand =>

                    _jobService.Recurring<IBrandGeneratorJob>(
                            Guid.NewGuid().ToString(),
                            x => x.CleanAsync(default),
                            jobType.ToString(),
                            runTime),

                    // _jobService.AddOrUpdate(Guid.NewGuid().ToString(), new DeleteRandomBrands(), jobType.ToString(), startTime),

                CommandList.DummyHangeFireJob =>
                    _jobService.AddOrUpdate(
                       Guid.NewGuid().ToString(),
                       new GenerateRandomBrands { NSeed = repeatTimes ?? 1 },
                       jobType.ToString(),
                       runTime),
                CommandList.SendEmail =>
                    _jobService.Recurring<IDummyEmailService>(
                        Guid.NewGuid().ToString(),
                        x => x.SendEmail("Recurring Sample Job - ", DateTime.UtcNow.ToString()),
                        jobType.ToString(),
                        runTime),
                _ => Guid.Empty.ToString(),
            },

            _ => Guid.Empty.ToString(),
        };
    }
}