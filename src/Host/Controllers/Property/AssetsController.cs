using FSH.WebApi.Application.Property.Assets;

namespace FSH.WebApi.Host.Controllers.Property;

public class AssetsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Assets)]
    [OpenApiOperation("Search Assets using available filters.", "")]
    public Task<PaginationResponse<AssetDto>> SearchAsync(SearchAssetsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Assets)]
    [OpenApiOperation("Get asset details.", "")]
    public Task<AssetDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetAssetRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Assets)]
    [OpenApiOperation("Create a new asset.", "")]
    public Task<Guid> CreateAsync(CreateAssetRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Assets)]
    [OpenApiOperation("Update a asset.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateAssetRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Assets)]
    [OpenApiOperation("Delete a asset.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteAssetRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.Assets)]
    [OpenApiOperation("Export a Assets.", "")]
    public async Task<FileResult> ExportAsync(ExportAssetsRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "AssetExports");
    }

    [HttpPost("import")]
    [MustHavePermission(FSHAction.Import, FSHResource.Assets)]
    [OpenApiOperation("Import a Assets.", "")]
    public async Task<ActionResult<int>> ImportAsync(ImportAssetsRequest request)
    {
        return Ok(await Mediator.Send(request));
    }

    [HttpPost("exportform")]
    [MustHavePermission(FSHAction.Export, FSHResource.Assets)]
    [OpenApiOperation("Export a Assets Delivery Form.", "")]
    public async Task<FileResult> ExportFormAsync(ExportAssetsDeliveryRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "AssetsDeliveryForm");
    }
}