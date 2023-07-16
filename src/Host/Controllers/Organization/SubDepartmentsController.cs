using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Application.Organization.SubDepartments;

namespace FSH.WebApi.Host.Controllers.Organization;

public class SubDepartmentsController : VersionedApiController
{
    // private readonly IExcelReader _excelReader;
    // public SubDepartmentsController(IExcelReader excelReader)
    // {
    //    _excelReader = excelReader;
    // }

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.SubDepartments)]
    [OpenApiOperation("Search SubDepartments using available filters.", "")]
    public Task<PaginationResponse<SubDepartmentDto>> SearchAsync(SearchSubDepartmentsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.SubDepartments)]
    [OpenApiOperation("Get SubDepartment details.", "")]
    public Task<SubDepartmentDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetSubDepartmentRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.SubDepartments)]
    [OpenApiOperation("Create a new SubDepartment.", "")]
    public Task<Guid> CreateAsync(CreateSubDepartmentRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.SubDepartments)]
    [OpenApiOperation("Update a SubDepartment.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateSubDepartmentRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.SubDepartments)]
    [OpenApiOperation("Delete a SubDepartment.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteSubDepartmentRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.SubDepartments)]
    [OpenApiOperation("Export a SubDepartments.", "")]
    public async Task<FileResult> ExportAsync(ExportSubDepartmentsRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "SubDepartmentExports");
    }

    [HttpPost("import")]
    [MustHavePermission(FSHAction.Import, FSHResource.SubDepartments)]
    [OpenApiOperation("Import a SubDepartments.", "")]
    public async Task<ActionResult<int>> ImportAsync(ImportSubDepartmentsRequest request)
    {
        return Ok(await Mediator.Send(request));
    }
}