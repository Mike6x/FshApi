using FSH.WebApi.Application.Place.Retailers;

namespace FSH.WebApi.Host.Controllers.Organization;

public class RetailersController : VersionedApiController
{
    // private readonly IExcelReader _excelReader;
    // public RetailersController(IExcelReader excelReader)
    // {
    //    _excelReader = excelReader;
    // }

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Retailers)]
    [OpenApiOperation("Search Retailers using available filters.", "")]
    public Task<PaginationResponse<RetailerDto>> SearchAsync(SearchRetailersRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Retailers)]
    [OpenApiOperation("Get Retailer details.", "")]
    public Task<RetailerDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetRetailerRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Retailers)]
    [OpenApiOperation("Create a new Retailer.", "")]
    public Task<Guid> CreateAsync(CreateRetailerRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Retailers)]
    [OpenApiOperation("Update a Retailer.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateRetailerRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Retailers)]
    [OpenApiOperation("Delete a Retailer.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteRetailerRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.Retailers)]
    [OpenApiOperation("Export a Retailers.", "")]
    public async Task<FileResult> ExportAsync(ExportRetailersRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "RetailerExports");
    }

    [HttpPost("import")]
    [MustHavePermission(FSHAction.Import, FSHResource.Retailers)]
    [OpenApiOperation("Import a Retailers.", "")]
    public async Task<ActionResult<int>> ImportAsync(ImportRetailersRequest request)
    {
        return Ok(await Mediator.Send(request));
    }

}