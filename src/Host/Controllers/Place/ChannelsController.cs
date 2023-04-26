using FSH.WebApi.Application.Place.Channels;

namespace FSH.WebApi.Host.Controllers.Organization;

public class ChannelsController : VersionedApiController
{
    // private readonly IExcelReader _excelReader;
    // public ChannelsController(IExcelReader excelReader)
    // {
    //    _excelReader = excelReader;
    // }

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Channels)]
    [OpenApiOperation("Search Channels using available filters.", "")]
    public Task<PaginationResponse<ChannelDto>> SearchAsync(SearchChannelsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Channels)]
    [OpenApiOperation("Get Channel details.", "")]
    public Task<ChannelDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetChannelRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Channels)]
    [OpenApiOperation("Create a new Channel.", "")]
    public Task<Guid> CreateAsync(CreateChannelRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Channels)]
    [OpenApiOperation("Update a Channel.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateChannelRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Channels)]
    [OpenApiOperation("Delete a Channel.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteChannelRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.Channels)]
    [OpenApiOperation("Export a Channels.", "")]
    public async Task<FileResult> ExportAsync(ExportChannelsRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "ChannelExports");
    }

    [HttpPost("import")]
    [MustHavePermission(FSHAction.Import, FSHResource.Channels)]
    [OpenApiOperation("Import a Channels.", "")]
    public async Task<ActionResult<int>> ImportAsync(ImportChannelsRequest request)
    {
        return Ok(await Mediator.Send(request));
    }

}