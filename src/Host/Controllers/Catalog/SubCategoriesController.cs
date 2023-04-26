using FSH.WebApi.Application.Catalog.SubCategories;

namespace FSH.WebApi.Host.Controllers.Catalog;

public class SubCategoriesController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.SubCategories)]
    [OpenApiOperation("Search SubCategories using available filters.", "")]
    public Task<PaginationResponse<SubCategorieDto>> SearchAsync(SearchSubCategoriesRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.SubCategories)]
    [OpenApiOperation("Get category details.", "")]
    public Task<SubCategorieDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetSubCategorieRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.SubCategories)]
    [OpenApiOperation("Create a new category.", "")]
    public Task<Guid> CreateAsync(CreateSubCategorieRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.SubCategories)]
    [OpenApiOperation("Update a category.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateSubCategorieRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.SubCategories)]
    [OpenApiOperation("Delete a category.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteSubCategorieRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.SubCategories)]
    [OpenApiOperation("Export a SubCategories.", "")]
    public async Task<FileResult> ExportAsync(ExportSubCategoriesRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "CategoryExports");
    }

    [HttpPost("import")]
    [MustHavePermission(FSHAction.Import, FSHResource.SubCategories)]
    [OpenApiOperation("Import a SubCategories.", "")]
    public async Task<ActionResult<int>> ImportAsync(ImportSubCategoriesRequest request)
    {
        return Ok(await Mediator.Send(request));
    }

}