using FSH.WebApi.Application.Catalog.BusinessLines;

namespace FSH.WebApi.Host.Controllers.Catalog;

public class BusinessLinesController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.BusinessLines)]
    [OpenApiOperation("Search BusinessLines using available filters.", "")]
    public Task<PaginationResponse<BusinessLineDto>> SearchAsync(SearchBusinessLinesRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.BusinessLines)]
    [OpenApiOperation("Get category details.", "")]
    public Task<BusinessLineDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetBusinessLineRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.BusinessLines)]
    [OpenApiOperation("Create a new category.", "")]
    public Task<Guid> CreateAsync(CreateBusinessLineRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.BusinessLines)]
    [OpenApiOperation("Update a category.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateBusinessLineRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.BusinessLines)]
    [OpenApiOperation("Delete a category.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteBusinessLineRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.BusinessLines)]
    [OpenApiOperation("Export a BusinessLines.", "")]
    public async Task<FileResult> ExportAsync(ExportBusinessLinesRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "CategoryExports");
    }

    [HttpPost("import")]
    [MustHavePermission(FSHAction.Import, FSHResource.BusinessLines)]
    [OpenApiOperation("Import a BusinessLines.", "")]
    public async Task<ActionResult<int>> ImportAsync(ImportBusinessLinesRequest request)
    {
        return Ok(await Mediator.Send(request));
    }

}