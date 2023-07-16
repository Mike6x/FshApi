using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Game;

namespace FSH.WebApi.Application.Game.VnPowerResults;

public class ExportVnPowerResultsRequest : BaseFilter, IRequest<Stream>
{
}

public class ExportVnPowerResultsRequestHandler : IRequestHandler<ExportVnPowerResultsRequest, Stream>
{
    private readonly IReadRepository<VnPowerResult> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportVnPowerResultsRequestHandler(IReadRepository<VnPowerResult> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportVnPowerResultsRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportVnPowerResultsSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportVnPowerResultsSpecification : EntitiesByBaseFilterSpec<VnPowerResult, VnPowerResultExportDto>
{
    public ExportVnPowerResultsSpecification(ExportVnPowerResultsRequest request)
        : base(request) =>
            Query
                .OrderByDescending(e => e.VnPower.DrawId)
                .SearchBy(request);
}