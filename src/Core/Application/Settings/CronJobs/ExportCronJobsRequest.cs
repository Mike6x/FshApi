using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.CronJobs;

public class ExportCronJobsRequest : BaseFilter, IRequest<Stream>
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}

public class ExportCronJobsRequestHandler(IReadRepository<CronJob> repository, IExcelWriter excelWriter)
    : IRequestHandler<ExportCronJobsRequest, Stream>
{
    private readonly IReadRepository<CronJob> _repository = repository;
    private readonly IExcelWriter _excelWriter = excelWriter;

    public async Task<Stream> Handle(ExportCronJobsRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportCronJobsSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportCronJobsSpecification : EntitiesByBaseFilterSpec<CronJob, CronJobExportDto>
{
    public ExportCronJobsSpecification(ExportCronJobsRequest request)
        : base(request) =>
        Query
            .Where(e => e.FromDate >= request.FromDate, request.FromDate.HasValue)
            .Where(e => e.ToDate <= request.ToDate, request.ToDate.HasValue)
                .OrderByDescending(e => e.LastModifiedOn);
}