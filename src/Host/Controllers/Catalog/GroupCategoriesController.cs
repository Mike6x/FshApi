using FSH.WebApi.Application.Catalog.GroupCategories;

namespace FSH.WebApi.Host.Controllers.Catalog;

public class GroupCategoriesController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.GroupCategories)]
    [OpenApiOperation("Search GroupCategories using available filters.", "")]
    public Task<PaginationResponse<GroupCategorieDto>> SearchAsync(SearchGroupCategoriesRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.GroupCategories)]
    [OpenApiOperation("Get category details.", "")]
    public Task<GroupCategorieDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetGroupCategorieRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.GroupCategories)]
    [OpenApiOperation("Create a new category.", "")]
    public Task<Guid> CreateAsync(CreateGroupCategorieRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.GroupCategories)]
    [OpenApiOperation("Update a category.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateGroupCategorieRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.GroupCategories)]
    [OpenApiOperation("Delete a category.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteGroupCategorieRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.GroupCategories)]
    [OpenApiOperation("Export a GroupCategories.", "")]
    public async Task<FileResult> ExportAsync(ExportGroupCategoriesRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "CategoryExports");
    }

    [HttpPost("import")]
    [MustHavePermission(FSHAction.Import, FSHResource.GroupCategories)]
    [OpenApiOperation("Import a GroupCategories.", "")]
    public async Task<ActionResult<int>> ImportAsync(ImportGroupCategoriesRequest request)
    {
        return Ok(await Mediator.Send(request));
    }

}