using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.People;

namespace FSH.WebApi.Application.People.Titles;

public class ExportTitlesRequest : BaseFilter, IRequest<Stream>
{
}

public class ExportTitlesRequestHandler : IRequestHandler<ExportTitlesRequest, Stream>
{
    private readonly IReadRepository<Title> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportTitlesRequestHandler(IReadRepository<Title> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportTitlesRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportTitlesSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportTitlesSpecification : EntitiesByBaseFilterSpec<Title, TitleExportDto>
{
    public ExportTitlesSpecification(ExportTitlesRequest request)
        : base(request) =>
        Query
           .SearchBy(request);
}