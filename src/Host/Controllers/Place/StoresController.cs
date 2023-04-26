using FSH.WebApi.Application.Place.Stores;

namespace FSH.WebApi.Host.Controllers.Organization;

public class StoresController : VersionedApiController
{
    // private readonly IExcelReader _excelReader;
    // public StoresController(IExcelReader excelReader)
    // {
    //    _excelReader = excelReader;
    // }

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Stores)]
    [OpenApiOperation("Search Stores using available filters.", "")]
    public Task<PaginationResponse<StoreDto>> SearchAsync(SearchStoresRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Stores)]
    [OpenApiOperation("Get Store details.", "")]
    public Task<StoreDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetStoreRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Stores)]
    [OpenApiOperation("Create a new Store.", "")]
    public Task<Guid> CreateAsync(CreateStoreRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Stores)]
    [OpenApiOperation("Update a Store.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateStoreRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Stores)]
    [OpenApiOperation("Delete a Store.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteStoreRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.Stores)]
    [OpenApiOperation("Export a Stores.", "")]
    public async Task<FileResult> ExportAsync(ExportStoresRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "StoreExports");
    }

    [HttpPost("import")]
    [MustHavePermission(FSHAction.Import, FSHResource.Stores)]
    [OpenApiOperation("Import a Stores.", "")]
    public async Task<ActionResult<int>> ImportAsync(ImportStoresRequest request)
    {
        return Ok(await Mediator.Send(request));
    }
}