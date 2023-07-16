using FSH.WebApi.Application.Settings.Dimensions;

namespace FSH.WebApi.Host.Controllers.Settings;

public class DimensionsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Dimensions)]
    [OpenApiOperation("Search Dimensions using available filters.", "")]
    public Task<PaginationResponse<DimensionDto>> SearchAsync(SearchDimensionsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Dimensions)]
    [OpenApiOperation("Get Dimension details.", "")]
    public Task<DimensionDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetDimensionRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Dimensions)]
    [OpenApiOperation("Create a new Dimension.", "")]
    public Task<Guid> CreateAsync(CreateDimensionRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Dimensions)]
    [OpenApiOperation("Update a Dimension.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateDimensionRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Dimensions)]
    [OpenApiOperation("Delete a Dimension.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteDimensionRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.Dimensions)]
    [OpenApiOperation("Export a Dimensions.", "")]
    public async Task<FileResult> ExportAsync(ExportDimensionsRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "CategoryExports");
    }

    [HttpPost("import")]
    [MustHavePermission(FSHAction.Import, FSHResource.Dimensions)]
    [OpenApiOperation("Import a Dimensions.", "")]
    public async Task<ActionResult<int>> ImportAsync(ImportDimensionsRequest request)
    {
        return Ok(await Mediator.Send(request));
    }
}