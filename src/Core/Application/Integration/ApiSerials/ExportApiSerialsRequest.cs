using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Integration;

namespace FSH.WebApi.Application.Integration.ApiSerials;

public class ExportApiSerialsRequest : BaseFilter, IRequest<Stream>
{
    public Guid? CronJobId { get; set; }
    public string? PoNumber { get; set; }
    public string? ItemCode { get; set; } // Model, SKU
}

public class ExportApiSerialsRequestHandler(IReadRepository<ApiSerial> repository, IExcelWriter excelWriter)
    : IRequestHandler<ExportApiSerialsRequest, Stream>
{
    private readonly IReadRepository<ApiSerial> _repository = repository;
    private readonly IExcelWriter _excelWriter = excelWriter;

    public async Task<Stream> Handle(ExportApiSerialsRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportApiSerialsSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportApiSerialsSpecification : EntitiesByBaseFilterSpec<ApiSerial, ApiSerialExportDto>
{
    public ExportApiSerialsSpecification(ExportApiSerialsRequest request)
        : base(request) =>
        Query
            .Where(e => e.CronJobId.Equals(request.CronJobId!.Value), request.CronJobId.HasValue)
            .Where(e => string.IsNullOrEmpty(request.PoNumber) || e.PoNumber.Equals(request.PoNumber))
            .Where(e => string.IsNullOrEmpty(request.ItemCode) || e.ItemCode.Equals(request.ItemCode))
                .OrderByDescending(e => e.LastModifiedOn);
}