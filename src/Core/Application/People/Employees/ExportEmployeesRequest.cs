using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.People;

namespace FSH.WebApi.Application.People.Employees;

public class ExportEmployeesRequest : BaseFilter, IRequest<Stream>
{
    public DefaultIdType? DepartmentId { get; set; }
    public DefaultIdType? TeamId { get; set; }
}

public class ExportEmployeesRequestHandler : IRequestHandler<ExportEmployeesRequest, Stream>
{
    private readonly IReadRepository<Employee> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportEmployeesRequestHandler(IReadRepository<Employee> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportEmployeesRequest request, CancellationToken cancellationToken)
    {
        var spec = new EmployeesByExportRequestSpec(request);

        // var spec = new EmployeesByExportRequestWithTeamSpec(request);
        // var spec = new EmployeesByExportRequestWithDepartmentSpec(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}