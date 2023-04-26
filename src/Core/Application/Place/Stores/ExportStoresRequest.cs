using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Place;

namespace FSH.WebApi.Application.Place.Stores;

public class ExportStoresRequest : BaseFilter, IRequest<Stream>
{
    public Guid? RetailerId { get; set; }
}

public class ExportStoresRequestHandler : IRequestHandler<ExportStoresRequest, Stream>
{
    private readonly IReadRepository<Store> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportStoresRequestHandler(IReadRepository<Store> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportStoresRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportStoresSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportStoresSpecification : EntitiesByBaseFilterSpec<Store, StoreExportDto>
{
    public ExportStoresSpecification(ExportStoresRequest request)
        : base(request) =>
        Query
            .Include(e => e.Retailer)
            .Where(e => e.RetailerId.Equals(request.RetailerId!.Value), request.RetailerId.HasValue);
}