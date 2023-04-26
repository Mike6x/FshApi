using FSH.WebApi.Application.Property.AssetHistorys;

namespace FSH.WebApi.Host.Controllers.Property;

public class AssetHistorysController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.AssetHistorys)]
    [OpenApiOperation("Search AssetHistorys using available filters.", "")]
    public Task<PaginationResponse<AssetHistoryDto>> SearchAsync(SearchAssetHistorysRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.AssetHistorys)]
    [OpenApiOperation("Get category details.", "")]
    public Task<AssetHistoryDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetAssetHistoryRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.AssetHistorys)]
    [OpenApiOperation("Create a new category.", "")]
    public Task<Guid> CreateAsync(CreateAssetHistoryRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.AssetHistorys)]
    [OpenApiOperation("Update a category.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateAssetHistoryRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.AssetHistorys)]
    [OpenApiOperation("Delete a category.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteAssetHistoryRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.AssetHistorys)]
    [OpenApiOperation("Export a AssetHistorys.", "")]
    public async Task<FileResult> ExportAsync(ExportAssetHistorysRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "CategoryExports");
    }

    [HttpPost("import")]
    [MustHavePermission(FSHAction.Import, FSHResource.AssetHistorys)]
    [OpenApiOperation("Import a AssetHistorys.", "")]
    public async Task<ActionResult<int>> ImportAsync(ImportAssetHistorysRequest request)
    {
        return Ok(await Mediator.Send(request));
    }
}