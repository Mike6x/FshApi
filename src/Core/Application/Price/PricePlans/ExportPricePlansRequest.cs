using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Price;

namespace FSH.WebApi.Application.Price.PricePlans;

public class ExportPricePlansRequest : BaseFilter, IRequest<Stream>
{
    public Guid? PriceGroupId { get; set; }
}

public class ExportPricePlansRequestHandler : IRequestHandler<ExportPricePlansRequest, Stream>
{
    private readonly IReadRepository<PricePlan> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportPricePlansRequestHandler(IReadRepository<PricePlan> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportPricePlansRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportPricePlansSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportPricePlansSpecification : EntitiesByBaseFilterSpec<PricePlan, PricePlanExportDto>
{
    public ExportPricePlansSpecification(ExportPricePlansRequest request)
        : base(request) =>
        Query
            .Include(e => e.PriceGroup)
            .Where(e => e.PriceGroupId.Equals(request.PriceGroupId!.Value), request.PriceGroupId.HasValue);
}