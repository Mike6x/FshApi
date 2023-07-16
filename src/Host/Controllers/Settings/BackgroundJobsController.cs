using FSH.WebApi.Application.Settings.BackgroundJobs;

namespace FSH.WebApi.Host.Controllers.Settings;

public class BackgroundJobsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.BackgroundJobs)]
    [OpenApiOperation("Search BackgroundJobs using available filters.", "")]
    public Task<PaginationResponse<BackgroundJobDto>> SearchAsync(SearchBackgroundJobsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.BackgroundJobs)]
    [OpenApiOperation("Get BackgroundJob details.", "")]
    public Task<BackgroundJobDetailsDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new GetBackgroundJobRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.BackgroundJobs)]
    [OpenApiOperation("Create a new BackgroundJob.", "")]
    public Task<DefaultIdType> CreateAsync(CreateBackgroundJobRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.BackgroundJobs)]
    [OpenApiOperation("Update a BackgroundJob.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(UpdateBackgroundJobRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.BackgroundJobs)]
    [OpenApiOperation("Delete a BackgroundJob.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new DeleteBackgroundJobRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.BackgroundJobs)]
    [OpenApiOperation("Export a BackgroundJobs.", "")]
    public async Task<FileResult> ExportAsync(ExportBackgroundJobsRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "CategoryExports");
    }

    [HttpPost("import")]
    [MustHavePermission(FSHAction.Import, FSHResource.BackgroundJobs)]
    [OpenApiOperation("Import a BackgroundJobs.", "")]
    public async Task<ActionResult<int>> ImportAsync(ImportBackgroundJobsRequest request)
    {
        return Ok(await Mediator.Send(request));
    }
}