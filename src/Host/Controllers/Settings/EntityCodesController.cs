using FSH.WebApi.Application.Settings.EntityCodes;

namespace FSH.WebApi.Host.Controllers.Settings;

public class EntityCodesController : VersionedApiController
{
    // private readonly IExcelReader _excelReader;
    // public EntityCodesController(IExcelReader excelReader)
    // {
    //    _excelReader = excelReader;
    // }

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.EntityCodes)]
    [OpenApiOperation("Search EntityCodes using available filters.", "")]
    public Task<PaginationResponse<EntityCodeDto>> SearchAsync(SearchEntityCodesRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.EntityCodes)]
    [OpenApiOperation("Get EntityCode details.", "")]
    public Task<EntityCodeDetailsDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new GetEntityCodeRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.EntityCodes)]
    [OpenApiOperation("Create a new EntityCode.", "")]
    public Task<DefaultIdType> CreateAsync(CreateEntityCodeRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.EntityCodes)]
    [OpenApiOperation("Update a EntityCode.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(UpdateEntityCodeRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.EntityCodes)]
    [OpenApiOperation("Delete a EntityCode.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new DeleteEntityCodeRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.EntityCodes)]
    [OpenApiOperation("Export a EntityCodes.", "")]
    public async Task<FileResult> ExportAsync(ExportEntityCodesRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "EntityCodeExports");
    }

    [HttpPost("import")]
    [MustHavePermission(FSHAction.Import, FSHResource.EntityCodes)]
    [OpenApiOperation("Import a EntityCodes.", "")]
    public async Task<ActionResult<int>> ImportAsync(ImportEntityCodesRequest request)
    {
        return Ok(await Mediator.Send(request));
    }
}