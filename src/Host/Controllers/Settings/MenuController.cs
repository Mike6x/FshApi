using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Application.Settings.Menus;

namespace FSH.WebApi.Host.Controllers.Settings;

public class MenusController : VersionedApiController
{
    // private readonly IExcelReader _excelReader;
    // public MenusController(IExcelReader excelReader)
    // {
    //    _excelReader = excelReader;
    // }

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Menus)]
    [OpenApiOperation("Search Menus using available filters.", "")]
    public Task<PaginationResponse<MenuDto>> SearchAsync(SearchMenusRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Menus)]
    [OpenApiOperation("Get Menu details.", "")]
    public Task<MenuDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetMenuRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Menus)]
    [OpenApiOperation("Create a new Menu.", "")]
    public Task<Guid> CreateAsync(CreateMenuRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Menus)]
    [OpenApiOperation("Update a Menu.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateMenuRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Menus)]
    [OpenApiOperation("Delete a Menu.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteMenuRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.Menus)]
    [OpenApiOperation("Export a Menus.", "")]
    public async Task<FileResult> ExportAsync(ExportMenusRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "MenuExports");
    }

    [HttpPost("import")]
    [MustHavePermission(FSHAction.Import, FSHResource.Menus)]
    [OpenApiOperation("Import a Menus.", "")]
    public async Task<ActionResult<int>> ImportAsync(ImportMenusRequest request)
    {
        return Ok(await Mediator.Send(request));
    }
}