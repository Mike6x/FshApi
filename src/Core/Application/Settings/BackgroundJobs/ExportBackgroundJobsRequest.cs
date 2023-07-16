using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.BackgroundJobs;

public class ExportBackgroundJobsRequest : BaseFilter, IRequest<Stream>
{
    public BackgroundJobType? Type { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}

public class ExportBackgroundJobsRequestHandler(
    IReadRepository<BackgroundJob> repository,
    IExcelWriter excelWriter) : IRequestHandler<ExportBackgroundJobsRequest, Stream>
{
    private readonly IReadRepository<BackgroundJob> _repository = repository;
    private readonly IExcelWriter _excelWriter = excelWriter;

    public async Task<Stream> Handle(ExportBackgroundJobsRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportBackgroundJobsSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportBackgroundJobsSpecification : EntitiesByBaseFilterSpec<BackgroundJob, BackgroundJobExportDto>
{
    public ExportBackgroundJobsSpecification(ExportBackgroundJobsRequest request)
        : base(request) =>
        Query
            .Where(e => e.Type.Equals(request.Type!.Value), request.Type.HasValue)
            .Where(e => e.FromDate >= request.FromDate, request.FromDate.HasValue)
            .Where(e => e.ToDate <= request.ToDate, request.ToDate.HasValue)
                .OrderByDescending(e => e.LastModifiedBy);
}