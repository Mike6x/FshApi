using FSH.WebApi.Application.Leave.LeaveAllocations;

namespace FSH.WebApi.Host.Controllers.Leave;

public class LeaveAllocationsController : VersionedApiController
{
    [HttpPost("GetList")]
    [MustHavePermission(FSHAction.Search, FSHResource.LeaveAllocations)]
    [OpenApiOperation("Get LeaveAllocations for Employee .", "")]
    public async Task<List<LeaveAllocationDto>> GetListAsync(GetLeaveAllocationsRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.LeaveAllocations)]
    [OpenApiOperation("Search LeaveAllocations using available filters.", "")]
    public Task<PaginationResponse<LeaveAllocationDto>> SearchAsync(SearchLeaveAllocationsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.LeaveAllocations)]
    [OpenApiOperation("Get LeaveAllocation details.", "")]
    public Task<LeaveAllocationDetailsDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new GetLeaveAllocationRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.LeaveAllocations)]
    [OpenApiOperation("Create a new LeaveAllocation.", "")]
    public Task<DefaultIdType> CreateAsync(CreateLeaveAllocationRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.LeaveAllocations)]
    [OpenApiOperation("Update a LeaveAllocation.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(UpdateLeaveAllocationRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpPut("{id:guid}/unlock")]
    [MustHavePermission(FSHAction.Unlock, FSHResource.LeaveAllocations)]
    [OpenApiOperation("Log and unlock a LeaveAllocation.", "")]
    public async Task<ActionResult<DefaultIdType>> UnlockAsync(UnlockLeaveAllocationRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.LeaveAllocations)]
    [OpenApiOperation("Delete a LeaveAllocation.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new DeleteLeaveAllocationRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.LeaveAllocations)]
    [OpenApiOperation("Export a LeaveAllocations.", "")]
    public async Task<FileResult> ExportAsync(ExportLeaveAllocationsRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "LeaveAllocationExports");
    }

    [HttpPost("import")]
    [MustHavePermission(FSHAction.Import, FSHResource.LeaveAllocations)]
    [OpenApiOperation("Import a LeaveAllocations.", "")]
    public async Task<ActionResult<int>> ImportAsync(ImportLeaveAllocationsRequest request)
    {
        return Ok(await Mediator.Send(request));
    }
}