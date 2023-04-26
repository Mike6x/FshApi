using FSH.WebApi.Application.Catalog.Categories;

namespace FSH.WebApi.Host.Controllers.Catalog;

public class CategoriesController : VersionedApiController
{
    // private readonly IExcelReader _excelReader;
    // public CategoriesController(IExcelReader excelReader)
    // {
    //    _excelReader = excelReader;
    // }

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Categories)]
    [OpenApiOperation("Search categories using available filters.", "")]
    public Task<PaginationResponse<CategorieDto>> SearchAsync(SearchCategoriesRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Categories)]
    [OpenApiOperation("Get category details.", "")]
    public Task<CategorieDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetCategorieRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Categories)]
    [OpenApiOperation("Create a new category.", "")]
    public Task<Guid> CreateAsync(CreateCategorieRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Categories)]
    [OpenApiOperation("Update a category.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateCategorieRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Categories)]
    [OpenApiOperation("Delete a category.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteCategorieRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.Categories)]
    [OpenApiOperation("Export a categories.", "")]
    public async Task<FileResult> ExportAsync(ExportCategoriesRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "CategoryExports");
    }

    [HttpPost("import")]
    [MustHavePermission(FSHAction.Import, FSHResource.Categories)]
    [OpenApiOperation("Import a categories.", "")]
    public async Task<ActionResult<int>> ImportAsync(ImportCategoriesRequest request)
    {
        return Ok(await Mediator.Send(request));
    }

}