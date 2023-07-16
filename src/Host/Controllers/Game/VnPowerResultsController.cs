using FSH.WebApi.Application.Game.VnPowerResults;
using FSH.WebApi.Application.Game.VnPowers;

namespace FSH.WebApi.Host.Controllers.Game;

public class VnPowerResultsController : VersionedApiController
{
    [HttpPost("GetList")]
    [MustHavePermission(FSHAction.View, FSHResource.VnPowers)]
    [OpenApiOperation("Get VnPowerResults Lists by RoundId.", "")]
    public async Task<List<VnPowerResultDto>> GetListAsync(GetVnPowerResultsRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.VnPowers)]
    [OpenApiOperation("Search VnPowerResults using available filters.", "")]
    public Task<PaginationResponse<VnPowerResultDto>> SearchAsync(SearchVnPowerResultsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.VnPowers)]
    [OpenApiOperation("Get VnPowerResult details.", "")]
    public Task<VnPowerResultDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetVnPowerResultRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.VnPowers)]
    [OpenApiOperation("Create a new VnPowerResult.", "")]
    public Task<Guid> CreateAsync(CreateVnPowerResultRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.VnPowers)]
    [OpenApiOperation("Update a VnPowerResult.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateVnPowerResultRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.VnPowers)]
    [OpenApiOperation("Delete a VnPowerResult.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteVnPowerResultRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.VnPowers)]
    [OpenApiOperation("Export a VnPowerResults.", "")]
    public async Task<FileResult> ExportAsync(ExportVnPowerResultsRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "VnPowerResultExports");
    }

    [HttpPost("import")]
    [MustHavePermission(FSHAction.Import, FSHResource.VnPowers)]
    [OpenApiOperation("Import a VnPowerResults.", "")]
    public async Task<ActionResult<int>> ImportAsync(ImportVnPowerResultsRequest request)
    {
        return Ok(await Mediator.Send(request));
    }
}
