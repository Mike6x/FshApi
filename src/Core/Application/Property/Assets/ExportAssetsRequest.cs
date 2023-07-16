using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Property;

namespace FSH.WebApi.Application.Property.Assets;

public class ExportAssetsRequest : BaseFilter, IRequest<Stream>
{
    public DefaultIdType? CategorieId { get; set; }
    public DefaultIdType? SubCategorieId { get; set; }
    public DefaultIdType? QualityStatusId { get; set; }
    public DefaultIdType? UsingStatusId { get; set; }
}

public class ExportAssetsRequestHandler : IRequestHandler<ExportAssetsRequest, Stream>
{
    private readonly IReadRepository<Asset> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportAssetsRequestHandler(IReadRepository<Asset> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportAssetsRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportAssetsSpec(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportAssetsSpec : EntitiesByBaseFilterSpec<Asset, AssetExportDto>
{
    public ExportAssetsSpec(ExportAssetsRequest request)
        : base(request) =>
            Query
                .Include(e => e.Categorie)
                    .Where(e => e.CategorieId.Equals(request.CategorieId!.Value), request.CategorieId.HasValue)
                    .Where(e => e.SubCategorieId.Equals(request.SubCategorieId!.Value), request.SubCategorieId.HasValue)
                    .Where(e => e.QualityStatusId.Equals(request.QualityStatusId!.Value), request.QualityStatusId.HasValue)
                    .Where(e => e.UsingStatusId.Equals(request.UsingStatusId!.Value), request.UsingStatusId.HasValue)
                        .OrderBy(e => e.Name);
}