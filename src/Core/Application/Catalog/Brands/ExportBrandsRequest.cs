using FSH.WebApi.Application.Common.DataIO;

namespace FSH.WebApi.Application.Catalog.Brands;

public class ExportBrandsRequest : BaseFilter, IRequest<Stream>
{
}

public class ExportBrandsRequestHandler : IRequestHandler<ExportBrandsRequest, Stream>
{
    private readonly IReadRepository<Brand> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportBrandsRequestHandler(IReadRepository<Brand> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportBrandsRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportBrandsSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportBrandsSpecification : EntitiesByBaseFilterSpec<Brand, BrandExportDto>
{
    public ExportBrandsSpecification(ExportBrandsRequest request)
        : base(request) =>
        Query
           .SearchBy(request);

}