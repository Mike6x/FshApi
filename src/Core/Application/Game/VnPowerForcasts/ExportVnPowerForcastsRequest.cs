using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Game;

namespace FSH.WebApi.Application.Game.VnPowerForcasts;

public class ExportVnPowerForcastsRequest : BaseFilter, IRequest<Stream>
{
}

public class ExportVnPowerForcastsRequestHandler : IRequestHandler<ExportVnPowerForcastsRequest, Stream>
{
    private readonly IReadRepository<VnPowerForcast> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportVnPowerForcastsRequestHandler(IReadRepository<VnPowerForcast> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportVnPowerForcastsRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportVnPowerForcastsSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportVnPowerForcastsSpecification : EntitiesByBaseFilterSpec<VnPowerForcast, VnPowerForcastExportDto>
{
    public ExportVnPowerForcastsSpecification(ExportVnPowerForcastsRequest request)
        : base(request) =>
            Query
                .OrderByDescending(e => e.VnPower.DrawId)
                .SearchBy(request);
}