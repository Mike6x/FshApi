using FSH.WebApi.Application.Common.DataIO;
namespace FSH.WebApi.Application.Catalog.BusinessLines;

public class ExportBusinessLinesRequest : BaseFilter, IRequest<Stream>
{
}

public class ExportBusinessLinesRequestHandler : IRequestHandler<ExportBusinessLinesRequest, Stream>
{
    private readonly IReadRepository<BusinessLine> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportBusinessLinesRequestHandler(IReadRepository<BusinessLine> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportBusinessLinesRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportBusinessLinesSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportBusinessLinesSpecification : EntitiesByBaseFilterSpec<BusinessLine, BusinessLineExportDto>
{
    public ExportBusinessLinesSpecification(ExportBusinessLinesRequest request)
        : base(request) =>
        Query
           .SearchBy(request);
}