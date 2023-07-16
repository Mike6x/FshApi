using FSH.WebApi.Application.Settings.CronJobs;

namespace FSH.WebApi.Host.Controllers.Settings;

public class CronJobsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.CronJobs)]
    [OpenApiOperation("Search CronJobs using available filters.", "")]
    public Task<PaginationResponse<CronJobDto>> SearchAsync(SearchCronJobsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.CronJobs)]
    [OpenApiOperation("Get CronJob details.", "")]
    public Task<CronJobDetailsDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new GetCronJobRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.CronJobs)]
    [OpenApiOperation("Create a new CronJob.", "")]
    public Task<DefaultIdType> CreateAsync(CreateCronJobRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.CronJobs)]
    [OpenApiOperation("Update a CronJob.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(UpdateCronJobRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.CronJobs)]
    [OpenApiOperation("Delete a CronJob.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new DeleteCronJobRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.CronJobs)]
    [OpenApiOperation("Export a CronJobs.", "")]
    public async Task<FileResult> ExportAsync(ExportCronJobsRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "CategoryExports");
    }

    [HttpPost("import")]
    [MustHavePermission(FSHAction.Import, FSHResource.CronJobs)]
    [OpenApiOperation("Import a CronJobs.", "")]
    public async Task<ActionResult<int>> ImportAsync(ImportCronJobsRequest request)
    {
        return Ok(await Mediator.Send(request));
    }
}