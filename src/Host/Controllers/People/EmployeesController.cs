using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Application.Organization.Teams;

namespace FSH.WebApi.Host.Controllers.People;

public class TeamsController : VersionedApiController
{
    private readonly IExcelReader _excelReader;
    public TeamsController(IExcelReader excelReader)
    {
        _excelReader = excelReader;
    }

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Teams)]
    [OpenApiOperation("Search Teams using available filters.", "")]
    public Task<PaginationResponse<TeamDto>> SearchAsync(SearchTeamsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Teams)]
    [OpenApiOperation("Get Team details.", "")]
    public Task<TeamDetailsDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new GetTeamRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Teams)]
    [OpenApiOperation("Create a new Team.", "")]
    public Task<DefaultIdType> CreateAsync(CreateTeamRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Teams)]
    [OpenApiOperation("Update a Team.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(UpdateTeamRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Teams)]
    [OpenApiOperation("Delete a Team.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new DeleteTeamRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.Teams)]
    [OpenApiOperation("Export a Teams.", "")]
    public async Task<FileResult> ExportAsync(ExportTeamsRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "TeamExports");
    }

    [HttpPost("import")]
    [MustHavePermission(FSHAction.Import, FSHResource.Teams)]
    [OpenApiOperation("Import a Teams.", "")]
    public async Task<ActionResult<int>> ImportAsync(ImportTeamsRequest request)
    {
        return Ok(await Mediator.Send(request));
    }

}