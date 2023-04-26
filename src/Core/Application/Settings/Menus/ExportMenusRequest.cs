using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.Menus;

public class ExportMenusRequest : BaseFilter, IRequest<Stream>
{
}

public class ExportMenusRequestHandler : IRequestHandler<ExportMenusRequest, Stream>
{
    private readonly IReadRepository<Menu> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportMenusRequestHandler(IReadRepository<Menu> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportMenusRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportMenusRequestSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportMenusRequestSpecification : EntitiesByBaseFilterSpec<Menu, MenuExportDto>
{
    public ExportMenusRequestSpecification(ExportMenusRequest request)
        : base(request) =>
        Query
           .SearchBy(request);

}