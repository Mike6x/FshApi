using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Place;

namespace FSH.WebApi.Application.Place.Retailers;

public class ExportRetailersRequest : BaseFilter, IRequest<Stream>
{
    public Guid? ChannelId { get; set; }
}

public class ExportRetailersRequestHandler : IRequestHandler<ExportRetailersRequest, Stream>
{
    private readonly IReadRepository<Retailer> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportRetailersRequestHandler(IReadRepository<Retailer> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportRetailersRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportRetailersSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportRetailersSpecification : EntitiesByBaseFilterSpec<Retailer, RetailerExportDto>
{
    public ExportRetailersSpecification(ExportRetailersRequest request)
        : base(request) =>
        Query
            .Include(e => e.Channel)
            .Where(e => e.ChannelId.Equals(request.ChannelId!.Value), request.ChannelId.HasValue);
}