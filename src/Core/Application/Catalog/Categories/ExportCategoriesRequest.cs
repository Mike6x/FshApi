using FSH.WebApi.Application.Common.DataIO;

namespace FSH.WebApi.Application.Catalog.Categories;

public class ExportCategoriesRequest : BaseFilter, IRequest<Stream>
{
    public DefaultIdType? GroupCategorieId { get; set; }
    public CatalogType? Type { get; set; }
}

public class ExportCategoriesRequestHandler(IReadRepository<Categorie> repository, IExcelWriter excelWriter)
    : IRequestHandler<ExportCategoriesRequest, Stream>
{
    private readonly IReadRepository<Categorie> _repository = repository;
    private readonly IExcelWriter _excelWriter = excelWriter;

    public async Task<Stream> Handle(ExportCategoriesRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportCategoriesSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportCategoriesSpecification : EntitiesByBaseFilterSpec<Categorie, CategorieExportDto>
{
    public ExportCategoriesSpecification(ExportCategoriesRequest request)
        : base(request) =>
        Query
            .Include(e => e.GroupCategorie)
                .Where(e => e.GroupCategorieId.Equals(request.GroupCategorieId!.Value), request.GroupCategorieId.HasValue)
                .Where(e => e.Type.Equals(request.Type!.Value), request.Type.HasValue)
                    .OrderBy(e => e.Order);
}