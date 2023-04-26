using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Price;

namespace FSH.WebApi.Application.Price.PriceGroups;

public class ExportPriceGroupsRequest : BaseFilter, IRequest<Stream>
{
}

public class ExportPriceGroupsRequestHandler : IRequestHandler<ExportPriceGroupsRequest, Stream>
{
    private readonly IReadRepository<PriceGroup> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportPriceGroupsRequestHandler(IReadRepository<PriceGroup> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportPriceGroupsRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportPriceGroupsSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportPriceGroupsSpecification : EntitiesByBaseFilterSpec<PriceGroup, PriceGroupExportDto>
{
    public ExportPriceGroupsSpecification(ExportPriceGroupsRequest request)
        : base(request) =>
        Query
           .SearchBy(request);

}