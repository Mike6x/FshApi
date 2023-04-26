using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Organization;

namespace FSH.WebApi.Application.Organization.Teams;

public class ExportTeamsRequest : BaseFilter, IRequest<Stream>
{
    public Guid? SubDepartmentId { get; set; }
}

public class ExportTeamsRequestHandler : IRequestHandler<ExportTeamsRequest, Stream>
{
    private readonly IReadRepository<Team> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportTeamsRequestHandler(IReadRepository<Team> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportTeamsRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportTeamsSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportTeamsSpecification : EntitiesByBaseFilterSpec<Team, TeamExportDto>
{
    public ExportTeamsSpecification(ExportTeamsRequest request)
        : base(request) =>
            Query
                .Include(e => e.SubDepartment)
                .Where(e => e.SubDepartmentId.Equals(request.SubDepartmentId!.Value), request.SubDepartmentId.HasValue);
}