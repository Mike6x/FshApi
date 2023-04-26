using FSH.WebApi.Application.Common.DataIO;

namespace FSH.WebApi.Application.Catalog.Categories;

public class ExportCategoriesRequest : BaseFilter, IRequest<Stream>
{
    public DefaultIdType? GroupCategorieId { get; set; }
}

public class ExportCategoriesRequestHandler : IRequestHandler<ExportCategoriesRequest, Stream>
{
    private readonly IReadRepository<Categorie> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportCategoriesRequestHandler(IReadRepository<Categorie> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

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
            .OrderBy(e => e.Order);
}