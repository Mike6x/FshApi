using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Application.Organization.Departments;

namespace FSH.WebApi.Host.Controllers.Organization;

public class DepartmentsController : VersionedApiController
{
    // private readonly IExcelReader _excelReader;
    // public DepartmentsController(IExcelReader excelReader)
    // {
    //    _excelReader = excelReader;
    // }

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Departments)]
    [OpenApiOperation("Search Departments using available filters.", "")]
    public Task<PaginationResponse<DepartmentDto>> SearchAsync(SearchDepartmentsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Departments)]
    [OpenApiOperation("Get Department details.", "")]
    public Task<DepartmentDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetDepartmentRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Departments)]
    [OpenApiOperation("Create a new Department.", "")]
    public Task<Guid> CreateAsync(CreateDepartmentRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Departments)]
    [OpenApiOperation("Update a Department.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateDepartmentRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Departments)]
    [OpenApiOperation("Delete a Department.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteDepartmentRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.Departments)]
    [OpenApiOperation("Export a Departments.", "")]
    public async Task<FileResult> ExportAsync(ExportDepartmentsRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "DepartmentExports");
    }

    [HttpPost("import")]
    [MustHavePermission(FSHAction.Import, FSHResource.Departments)]
    [OpenApiOperation("Import a Departments.", "")]
    public async Task<ActionResult<int>> ImportAsync(ImportDepartmentsRequest request)
    {
        return Ok(await Mediator.Send(request));
    }

}