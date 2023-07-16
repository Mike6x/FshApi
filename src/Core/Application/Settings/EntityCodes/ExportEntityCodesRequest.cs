using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.EntityCodes;

public class ExportEntityCodesRequest : BaseFilter, IRequest<Stream>
{
    public CodeType? Type { get; set; }
}

public class ExportEntityCodesRequestHandler(IReadRepository<EntityCode> repository, IExcelWriter excelWriter)
    : IRequestHandler<ExportEntityCodesRequest, Stream>
{
    private readonly IReadRepository<EntityCode> _repository = repository;
    private readonly IExcelWriter _excelWriter = excelWriter;

    public async Task<Stream> Handle(ExportEntityCodesRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportEntityCodesSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportEntityCodesSpecification : EntitiesByBaseFilterSpec<EntityCode, EntityCodeExportDto>
{
    public ExportEntityCodesSpecification(ExportEntityCodesRequest request)
        : base(request) =>
        Query
            .Where(e => e.Type.Equals(request.Type!.Value), request.Type.HasValue)
                .OrderBy(e => e.Order);
}