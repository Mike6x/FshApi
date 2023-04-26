using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Application.People.Employees;

namespace FSH.WebApi.Host.Controllers.Organization;

public class EmployeesController : VersionedApiController
{
    // private readonly IExcelReader _excelReader;
    // public EmployeesController(IExcelReader excelReader)
    // {
    //    _excelReader = excelReader;
    // }

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Employees)]
    [OpenApiOperation("Search Employees using available filters.", "")]
    public Task<PaginationResponse<EmployeeDto>> SearchAsync(SearchEmployeesRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Employees)]
    [OpenApiOperation("Get Employee details.", "")]
    public Task<EmployeeDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetEmployeeRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Employees)]
    [OpenApiOperation("Create a new Employee.", "")]
    public Task<Guid> CreateAsync(CreateEmployeeRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Employees)]
    [OpenApiOperation("Update a Employee.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateEmployeeRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Employees)]
    [OpenApiOperation("Delete a Employee.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteEmployeeRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.Employees)]
    [OpenApiOperation("Export a Employees.", "")]
    public async Task<FileResult> ExportAsync(ExportEmployeesRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "EmployeeExports");
    }

    [HttpPost("import")]
    [MustHavePermission(FSHAction.Import, FSHResource.Employees)]
    [OpenApiOperation("Import a Employees.", "")]
    public async Task<ActionResult<int>> ImportAsync(ImportEmployeesRequest request)
    {
        return Ok(await Mediator.Send(request));
    }

}