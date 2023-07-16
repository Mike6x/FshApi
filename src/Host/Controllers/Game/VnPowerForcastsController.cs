using FSH.WebApi.Application.Game.VnPowerForcasts;

namespace FSH.WebApi.Host.Controllers.Game;

public class VnPowerForcastsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.VnPowerForcasts)]
    [OpenApiOperation("Search VnPowerForcasts using available filters.", "")]
    public Task<PaginationResponse<VnPowerForcastDto>> SearchAsync(SearchVnPowerForcastsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.VnPowerForcasts)]
    [OpenApiOperation("Get VnPowerForcast details.", "")]
    public Task<VnPowerForcastDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetVnPowerForcastRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.VnPowerForcasts)]
    [OpenApiOperation("Create a new VnPowerForcast.", "")]
    public Task<Guid> CreateAsync(CreateVnPowerForcastRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.VnPowerForcasts)]
    [OpenApiOperation("Update a VnPowerForcast.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateVnPowerForcastRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.VnPowerForcasts)]
    [OpenApiOperation("Delete a VnPowerForcast.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteVnPowerForcastRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.VnPowerForcasts)]
    [OpenApiOperation("Export a VnPowerForcasts.", "")]
    public async Task<FileResult> ExportAsync(ExportVnPowerForcastsRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "VnPowerForcastExports");
    }

    [HttpPost("import")]
    [MustHavePermission(FSHAction.Import, FSHResource.VnPowerForcasts)]
    [OpenApiOperation("Import a VnPowerForcasts.", "")]
    public async Task<ActionResult<int>> ImportAsync(ImportVnPowerForcastsRequest request)
    {
        return Ok(await Mediator.Send(request));
    }
}
