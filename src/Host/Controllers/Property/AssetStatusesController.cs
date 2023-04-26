using FSH.WebApi.Application.Property.AssetStatuses;

namespace FSH.WebApi.Host.Controllers.Property;

public class AssetStatusesController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.AssetStatuses)]
    [OpenApiOperation("Search AssetStatuses using available filters.", "")]
    public Task<PaginationResponse<AssetStatusDto>> SearchAsync(SearchAssetStatusesRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.AssetStatuses)]
    [OpenApiOperation("Get AssetStatus details.", "")]
    public Task<AssetStatusDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetAssetStatusRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.AssetStatuses)]
    [OpenApiOperation("Create a new AssetStatus.", "")]
    public Task<Guid> CreateAsync(CreateAssetStatusRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.AssetStatuses)]
    [OpenApiOperation("Update a AssetStatus.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateAssetStatusRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.AssetStatuses)]
    [OpenApiOperation("Delete a AssetStatus.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteAssetStatusRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.AssetStatuses)]
    [OpenApiOperation("Export a AssetStatuses.", "")]
    public async Task<FileResult> ExportAsync(ExportAssetStatusesRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "CategoryExports");
    }

    [HttpPost("import")]
    [MustHavePermission(FSHAction.Import, FSHResource.AssetStatuses)]
    [OpenApiOperation("Import a AssetStatuses.", "")]
    public async Task<ActionResult<int>> ImportAsync(ImportAssetStatusesRequest request)
    {
        return Ok(await Mediator.Send(request));
    }

}