using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Organization;

namespace FSH.WebApi.Application.Organization.Departments;

public class ExportDepartmentsRequest : BaseFilter, IRequest<Stream>
{
    public Guid? BusinessUnitId { get; set; }
}

public class ExportDepartmentsRequestHandler : IRequestHandler<ExportDepartmentsRequest, Stream>
{
    private readonly IReadRepository<Department> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportDepartmentsRequestHandler(IReadRepository<Department> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportDepartmentsRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportDepartmentsSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportDepartmentsSpecification : EntitiesByBaseFilterSpec<Department, DepartmentExportDto>
{
    public ExportDepartmentsSpecification(ExportDepartmentsRequest request)
        : base(request) =>
        Query
            .Include(e => e.BusinessUnit)
            .Where(e => e.BusinessUnitId.Equals(request.BusinessUnitId!.Value), request.BusinessUnitId.HasValue);
}