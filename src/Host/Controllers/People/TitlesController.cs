using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Application.People.Titles;

namespace FSH.WebApi.Host.Controllers.People;

public class TitlesController : VersionedApiController
{
    private readonly IExcelReader _excelReader;
    public TitlesController(IExcelReader excelReader)
    {
        _excelReader = excelReader;
    }

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Titles)]
    [OpenApiOperation("Search Titles using available filters.", "")]
    public Task<PaginationResponse<TitleDto>> SearchAsync(SearchTitlesRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Titles)]
    [OpenApiOperation("Get Title details.", "")]
    public Task<TitleDetailsDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new GetTitleRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Titles)]
    [OpenApiOperation("Create a new Title.", "")]
    public Task<DefaultIdType> CreateAsync(CreateTitleRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Titles)]
    [OpenApiOperation("Update a Title.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(UpdateTitleRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Titles)]
    [OpenApiOperation("Delete a Title.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new DeleteTitleRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.Titles)]
    [OpenApiOperation("Export a Titles.", "")]
    public async Task<FileResult> ExportAsync(ExportTitlesRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "TitleExports");
    }

    [HttpPost("import")]
    [MustHavePermission(FSHAction.Import, FSHResource.Titles)]
    [OpenApiOperation("Import a Titles.", "")]
    public async Task<ActionResult<int>> ImportAsync(ImportTitlesRequest request)
    {
        return Ok(await Mediator.Send(request));
    }

}