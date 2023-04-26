using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Property;

namespace FSH.WebApi.Application.Property.AssetHistorys;

public class ExportAssetHistorysRequest : BaseFilter, IRequest<Stream>
{
    public DefaultIdType? AssetId { get; set; }
}

public class ExportAssetHistorysRequestHandler : IRequestHandler<ExportAssetHistorysRequest, Stream>
{
    private readonly IReadRepository<AssetHistory> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportAssetHistorysRequestHandler(IReadRepository<AssetHistory> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportAssetHistorysRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportAssetHistorysSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportAssetHistorysSpecification : EntitiesByBaseFilterSpec<AssetHistory, AssetHistoryExportDto>
{
    public ExportAssetHistorysSpecification(ExportAssetHistorysRequest request)
        : base(request) =>
            Query

                .Where(e => e.AssetId.Equals(request.AssetId!.Value), request.AssetId.HasValue)
                .OrderBy(e => e.AssetId);
}