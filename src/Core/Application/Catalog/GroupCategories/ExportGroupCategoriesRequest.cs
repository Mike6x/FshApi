using FSH.WebApi.Application.Common.DataIO;

namespace FSH.WebApi.Application.Catalog.GroupCategories;

public class ExportGroupCategoriesRequest : BaseFilter, IRequest<Stream>
{
    public DefaultIdType? BusinessLineId { get; set; }
    public CatalogType? Type { get; set; }
}

public class ExportGroupCategoriesRequestHandler : IRequestHandler<ExportGroupCategoriesRequest, Stream>
{
    private readonly IReadRepository<GroupCategorie> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportGroupCategoriesRequestHandler(IReadRepository<GroupCategorie> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportGroupCategoriesRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportGroupCategoriesSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportGroupCategoriesSpecification : EntitiesByBaseFilterSpec<GroupCategorie, GroupCategorieExportDto>
{
    public ExportGroupCategoriesSpecification(ExportGroupCategoriesRequest request)
        : base(request) =>
        Query
            .Include(e => e.BusinessLine)
                .Where(e => e.BusinessLineId.Equals(request.BusinessLineId!.Value), request.BusinessLineId.HasValue)
                .Where(e => e.Type.Equals(request.Type!.Value), request.Type.HasValue)
                    .OrderBy(e => e.Order);
}