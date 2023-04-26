using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.People;

namespace FSH.WebApi.Application.Organization.SubDepartments;

public class ExportSubDepartmentsRequest : BaseFilter, IRequest<Stream>
{
    public Guid? DepartmentId { get; set; }
}

public class ExportSubDepartmentsRequestHandler : IRequestHandler<ExportSubDepartmentsRequest, Stream>
{
    private readonly IReadRepository<SubDepartment> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportSubDepartmentsRequestHandler(IReadRepository<SubDepartment> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportSubDepartmentsRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportSubDepartmentsSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportSubDepartmentsSpecification : EntitiesByBaseFilterSpec<SubDepartment, SubDepartmentExportDto>
{
    public ExportSubDepartmentsSpecification(ExportSubDepartmentsRequest request)
        : base(request) =>
            Query
                .Include(e => e.Department)
                .Where(e => e.DepartmentId.Equals(request.DepartmentId!.Value), request.DepartmentId.HasValue);
}
