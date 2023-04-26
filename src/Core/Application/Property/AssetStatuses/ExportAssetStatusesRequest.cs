using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Property;

namespace FSH.WebApi.Application.Property.AssetStatuses;

public class ExportAssetStatusesRequest : BaseFilter, IRequest<Stream>
{
}

public class ExportAssetStatusesRequestHandler : IRequestHandler<ExportAssetStatusesRequest, Stream>
{
    private readonly IReadRepository<AssetStatus> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportAssetStatusesRequestHandler(IReadRepository<AssetStatus> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportAssetStatusesRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportAssetStatusesSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportAssetStatusesSpecification : EntitiesByBaseFilterSpec<AssetStatus, AssetStatusExportDto>
{
    public ExportAssetStatusesSpecification(ExportAssetStatusesRequest request)
        : base(request) =>
        Query
           .SearchBy(request);
}