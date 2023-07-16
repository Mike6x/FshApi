using FSH.WebApi.Application.Leave.LeaveApplications;

namespace FSH.WebApi.Host.Controllers.Leave;

public class LeaveApplicationsController : VersionedApiController
{
    [HttpPost("GetList")]
    [MustHavePermission(FSHAction.Search, FSHResource.LeaveApplications)]
    [OpenApiOperation("Get LeaveAllocations for Employee .", "")]
    public async Task<List<LeaveApplicationDto>> GetListAsync(GetLeaveApplicationsRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.LeaveApplications)]
    [OpenApiOperation("Search LeaveApplications using available filters.", "")]
    public Task<PaginationResponse<LeaveApplicationDto>> SearchAsync(SearchLeaveApplicationsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.LeaveApplications)]
    [OpenApiOperation("Get LeaveApplication details.", "")]
    public Task<LeaveApplicationDetailsDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new GetLeaveApplicationRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.LeaveApplications)]
    [OpenApiOperation("Create a new LeaveApplication.", "")]
    public Task<DefaultIdType> CreateAsync(CreateLeaveApplicationRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.LeaveApplications)]
    [OpenApiOperation("Update a LeaveApplication.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(UpdateLeaveApplicationRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpPut("{id:guid}/Submit")]
    [MustHavePermission(FSHAction.Update, FSHResource.LeaveApplications)]
    [OpenApiOperation("Submit a LeaveApplication.", "")]
    public async Task<ActionResult<DefaultIdType>> SubmitAsync(SubmitLeaveApplicationRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpPut("{id:guid}/Approve")]
    [MustHavePermission(FSHAction.Update, FSHResource.LeaveApplications)]
    [OpenApiOperation("Approve a LeaveApplication.", "")]
    public async Task<ActionResult<DefaultIdType>> ApproveAsync(ApproveLeaveApplicationRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.LeaveApplications)]
    [OpenApiOperation("Delete a LeaveApplication.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new DeleteLeaveApplicationRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.LeaveApplications)]
    [OpenApiOperation("Export a LeaveApplications.", "")]
    public async Task<FileResult> ExportAsync(ExportLeaveApplicationsRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "LeaveApplicationExports");
    }

    [HttpPost("import")]
    [MustHavePermission(FSHAction.Import, FSHResource.LeaveApplications)]
    [OpenApiOperation("Import a LeaveApplications.", "")]
    public async Task<ActionResult<int>> ImportAsync(ImportLeaveApplicationsRequest request)
    {
        return Ok(await Mediator.Send(request));
    }
}