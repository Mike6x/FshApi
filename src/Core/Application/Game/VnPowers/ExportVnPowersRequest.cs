using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Game;

namespace FSH.WebApi.Application.Game.VnPowers;

public class ExportVnPowersRequest : BaseFilter, IRequest<Stream>
{
}

public class ExportVnPowersRequestHandler : IRequestHandler<ExportVnPowersRequest, Stream>
{
    private readonly IReadRepository<VnPower> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportVnPowersRequestHandler(IReadRepository<VnPower> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportVnPowersRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportVnPowersSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportVnPowersSpecification : EntitiesByBaseFilterSpec<VnPower, VnPowerExportDto>
{
    public ExportVnPowersSpecification(ExportVnPowersRequest request)
        : base(request) =>
        Query
           .OrderByDescending(e => e.DrawId)
           .SearchBy(request);
}