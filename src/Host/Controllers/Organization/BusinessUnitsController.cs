using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Application.Organization.BusinessUnits;

namespace FSH.WebApi.Host.Controllers.Organization;

public class BusinessUnitsController : VersionedApiController
{
    // private readonly IExcelReader _excelReader;
    // public BusinessUnitsController(IExcelReader excelReader)
    // {
    //    _excelReader = excelReader;
    // }

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.BusinessUnits)]
    [OpenApiOperation("Search BusinessUnits using available filters.", "")]
    public Task<PaginationResponse<BusinessUnitDto>> SearchAsync(SearchBusinessUnitsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.BusinessUnits)]
    [OpenApiOperation("Get BusinessUnit details.", "")]
    public Task<BusinessUnitDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetBusinessUnitRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.BusinessUnits)]
    [OpenApiOperation("Create a new BusinessUnit.", "")]
    public Task<Guid> CreateAsync(CreateBusinessUnitRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.BusinessUnits)]
    [OpenApiOperation("Update a BusinessUnit.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateBusinessUnitRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.BusinessUnits)]
    [OpenApiOperation("Delete a BusinessUnit.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteBusinessUnitRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.BusinessUnits)]
    [OpenApiOperation("Export a BusinessUnits.", "")]
    public async Task<FileResult> ExportAsync(ExportBusinessUnitsRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "BusinessUnitExports");
    }

    [HttpPost("import")]
    [MustHavePermission(FSHAction.Import, FSHResource.BusinessUnits)]
    [OpenApiOperation("Import a BusinessUnits.", "")]
    public async Task<ActionResult<int>> ImportAsync(ImportBusinessUnitsRequest request)
    {
        return Ok(await Mediator.Send(request));
    }

}