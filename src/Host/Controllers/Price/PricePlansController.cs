using FSH.WebApi.Application.Price.PricePlans;

namespace FSH.WebApi.Host.Controllers.Organization;

public class PricePlansController : VersionedApiController
{
    // private readonly IExcelReader _excelReader;
    // public PricePlansController(IExcelReader excelReader)
    // {
    //    _excelReader = excelReader;
    // }

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.PricePlans)]
    [OpenApiOperation("Search PricePlans using available filters.", "")]
    public Task<PaginationResponse<PricePlanDto>> SearchAsync(SearchPricePlansRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.PricePlans)]
    [OpenApiOperation("Get PricePlan details.", "")]
    public Task<PricePlanDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetPricePlanRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.PricePlans)]
    [OpenApiOperation("Create a new PricePlan.", "")]
    public Task<Guid> CreateAsync(CreatePricePlanRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.PricePlans)]
    [OpenApiOperation("Update a PricePlan.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdatePricePlanRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.PricePlans)]
    [OpenApiOperation("Delete a PricePlan.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeletePricePlanRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.PricePlans)]
    [OpenApiOperation("Export a PricePlans.", "")]
    public async Task<FileResult> ExportAsync(ExportPricePlansRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "PricePlanExports");
    }

    [HttpPost("import")]
    [MustHavePermission(FSHAction.Import, FSHResource.PricePlans)]
    [OpenApiOperation("Import a PricePlans.", "")]
    public async Task<ActionResult<int>> ImportAsync(ImportPricePlansRequest request)
    {
        return Ok(await Mediator.Send(request));
    }

}