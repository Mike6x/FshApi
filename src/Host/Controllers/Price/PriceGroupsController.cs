using FSH.WebApi.Application.Price.PriceGroups;

namespace FSH.WebApi.Host.Controllers.Organization;

public class PriceGroupsController : VersionedApiController
{
    // private readonly IExcelReader _excelReader;
    // public PriceGroupsController(IExcelReader excelReader)
    // {
    //    _excelReader = excelReader;
    // }

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.PriceGroups)]
    [OpenApiOperation("Search PriceGroups using available filters.", "")]
    public Task<PaginationResponse<PriceGroupDto>> SearchAsync(SearchPriceGroupsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.PriceGroups)]
    [OpenApiOperation("Get PriceGroup details.", "")]
    public Task<PriceGroupDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetPriceGroupRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.PriceGroups)]
    [OpenApiOperation("Create a new PriceGroup.", "")]
    public Task<Guid> CreateAsync(CreatePriceGroupRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.PriceGroups)]
    [OpenApiOperation("Update a PriceGroup.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdatePriceGroupRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.PriceGroups)]
    [OpenApiOperation("Delete a PriceGroup.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeletePriceGroupRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.PriceGroups)]
    [OpenApiOperation("Export a PriceGroups.", "")]
    public async Task<FileResult> ExportAsync(ExportPriceGroupsRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "PriceGroupExports");
    }

    [HttpPost("import")]
    [MustHavePermission(FSHAction.Import, FSHResource.PriceGroups)]
    [OpenApiOperation("Import a PriceGroups.", "")]
    public async Task<ActionResult<int>> ImportAsync(ImportPriceGroupsRequest request)
    {
        return Ok(await Mediator.Send(request));
    }

}