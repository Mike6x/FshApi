using FSH.WebApi.Application.Integration.ApiSerials;

namespace FSH.WebApi.Host.Controllers.Integration;

public class ApiSerialsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.ApiSerials)]
    [OpenApiOperation("Search ApiSerials using available filters.", "")]
    public Task<PaginationResponse<ApiSerialDto>> SearchAsync(SearchApiSerialsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.ApiSerials)]
    [OpenApiOperation("Get ApiSerial details.", "")]
    public Task<ApiSerialDetailsDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new GetApiSerialRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.ApiSerials)]
    [OpenApiOperation("Create a new ApiSerial.", "")]
    public Task<DefaultIdType> CreateAsync(CreateApiSerialRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.ApiSerials)]
    [OpenApiOperation("Update a ApiSerial.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(UpdateApiSerialRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.ApiSerials)]
    [OpenApiOperation("Delete a ApiSerial.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new DeleteApiSerialRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.ApiSerials)]
    [OpenApiOperation("Export a ApiSerials.", "")]
    public async Task<FileResult> ExportAsync(ExportApiSerialsRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "ApiSerialExports");
    }

    [HttpPost("import")]
    [MustHavePermission(FSHAction.Import, FSHResource.ApiSerials)]
    [OpenApiOperation("Import a ApiSerials.", "")]
    public async Task<ActionResult<int>> ImportAsync(ImportApiSerialsRequest request)
    {
        return Ok(await Mediator.Send(request));
    }
}