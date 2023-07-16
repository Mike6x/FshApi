using FSH.WebApi.Application.Game.VnPowers;

namespace FSH.WebApi.Host.Controllers.Game;

public class VnPowersController : VersionedApiController
{
    [HttpPost("Forcast")]
    [MustHavePermission(FSHAction.View, FSHResource.VnPowers)]
    [OpenApiOperation("Get VnPower Forcast for next round.", "")]
    public async Task<DefaultIdType> ForcastVnPowerAsync(ForcastVnPowerRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpPost("GetList")]
    [MustHavePermission(FSHAction.View, FSHResource.VnPowers)]
    [OpenApiOperation("Get VnPower Results Lists upto the DrrawId .", "")]
    public async Task<List<VnPowerDto>> GetListAsync(GetVnPowersRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.VnPowers)]
    [OpenApiOperation("Search VnPowers using available filters.", "")]
    public Task<PaginationResponse<VnPowerDto>> SearchAsync(SearchVnPowersRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.VnPowers)]
    [OpenApiOperation("Get VnPower details.", "")]
    public Task<VnPowerDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetVnPowerRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.VnPowers)]
    [OpenApiOperation("Create a new VnPower.", "")]
    public Task<Guid> CreateAsync(CreateVnPowerRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.VnPowers)]
    [OpenApiOperation("Update a VnPower.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateVnPowerRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.VnPowers)]
    [OpenApiOperation("Delete a VnPower.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteVnPowerRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.VnPowers)]
    [OpenApiOperation("Export a VnPowers.", "")]
    public async Task<FileResult> ExportAsync(ExportVnPowersRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "VnPowerExports");
    }

    [HttpPost("import")]
    [MustHavePermission(FSHAction.Import, FSHResource.VnPowers)]
    [OpenApiOperation("Import a VnPowers.", "")]
    public async Task<ActionResult<int>> ImportAsync(ImportVnPowersRequest request)
    {
        return Ok(await Mediator.Send(request));
    }
}