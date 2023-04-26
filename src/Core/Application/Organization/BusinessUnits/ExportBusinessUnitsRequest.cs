using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Organization;

namespace FSH.WebApi.Application.Organization.BusinessUnits;

public class ExportBusinessUnitsRequest : BaseFilter, IRequest<Stream>
{
}

public class ExportBusinessUnitsRequestHandler : IRequestHandler<ExportBusinessUnitsRequest, Stream>
{
    private readonly IReadRepository<BusinessUnit> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportBusinessUnitsRequestHandler(IReadRepository<BusinessUnit> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportBusinessUnitsRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportBusinessUnitsSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportBusinessUnitsSpecification : EntitiesByBaseFilterSpec<BusinessUnit, BusinessUnitExportDto>
{
    public ExportBusinessUnitsSpecification(ExportBusinessUnitsRequest request)
        : base(request) =>
        Query
           .SearchBy(request);

}