using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.Dimensions;

public class ExportDimensionsRequest : BaseFilter, IRequest<Stream>
{
    public string? Type { get; set; }
}

public class ExportDimensionsRequestHandler(IReadRepository<Dimension> repository, IExcelWriter excelWriter) : IRequestHandler<ExportDimensionsRequest, Stream>
{
    private readonly IReadRepository<Dimension> _repository = repository;
    private readonly IExcelWriter _excelWriter = excelWriter;

    public async Task<Stream> Handle(ExportDimensionsRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportDimensionsSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportDimensionsSpecification : EntitiesByBaseFilterSpec<Dimension, DimensionExportDto>
{
    public ExportDimensionsSpecification(ExportDimensionsRequest request)
        : base(request) =>
            Query
                .Where(e => string.IsNullOrEmpty(request.Type) || e.Type.Equals(request.Type))
                .OrderBy(e => e.Order);
}