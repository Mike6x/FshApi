using FSH.WebApi.Application.Common.DataIO;

namespace FSH.WebApi.Application.Catalog.SubCategories;

public class ExportSubCategoriesRequest : BaseFilter, IRequest<Stream>
{
    public Guid? CategorieId { get; set; }
}

public class ExportSubCategoriesRequestHandler : IRequestHandler<ExportSubCategoriesRequest, Stream>
{
    private readonly IReadRepository<SubCategorie> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportSubCategoriesRequestHandler(IReadRepository<SubCategorie> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportSubCategoriesRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportSubCategoriesSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportSubCategoriesSpecification : EntitiesByBaseFilterSpec<SubCategorie, SubCategorieExportDto>
{
    public ExportSubCategoriesSpecification(ExportSubCategoriesRequest request)
        : base(request) =>
        Query
            .Include(e => e.Categorie)
            .Where(e => e.CategorieId.Equals(request.CategorieId!.Value), request.CategorieId.HasValue)
            .OrderBy(e => e.Order);
}