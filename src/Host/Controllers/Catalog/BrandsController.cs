using FSH.WebApi.Application.Catalog.Brands;
using FSH.WebApi.Application.Common.DataIO;

namespace FSH.WebApi.Host.Controllers.Catalog;

public class BrandsController : VersionedApiController
{
    private readonly IExcelReader _excelReader;
    public BrandsController(IExcelReader excelReader)
    {
        _excelReader = excelReader;
    }

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Brands)]
    [OpenApiOperation("Search brands using available filters.", "")]
    public Task<PaginationResponse<BrandDto>> SearchAsync(SearchBrandsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Brands)]
    [OpenApiOperation("Get brand details.", "")]
    public Task<BrandDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetBrandRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Brands)]
    [OpenApiOperation("Create a new brand.", "")]
    public Task<Guid> CreateAsync(CreateBrandRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Brands)]
    [OpenApiOperation("Update a brand.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateBrandRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Brands)]
    [OpenApiOperation("Delete a brand.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteBrandRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.Brands)]
    [OpenApiOperation("Export a brands.", "")]
    public async Task<FileResult> ExportAsync(ExportBrandsRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "BrandExports");
    }

    [HttpPost("import")]
    [MustHavePermission(FSHAction.Import, FSHResource.Brands)]
    [OpenApiOperation("Import a brands.", "")]
    public async Task<ActionResult<int>> ImportAsync(ImportBrandsRequest request)
    {
        return Ok(await Mediator.Send(request));
    }

    [HttpPost("generate-random")]
    [MustHavePermission(FSHAction.Generate, FSHResource.Brands)]
    [OpenApiOperation("Generate a number of random brands.", "")]
    public Task<string> GenerateRandomAsync(GenerateRandomBrandRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpDelete("delete-random")]
    [MustHavePermission(FSHAction.Clean, FSHResource.Brands)]
    [OpenApiOperation("Delete the brands generated with the generate-random call.", "")]
    [ApiConventionMethod(typeof(FSHApiConventions), nameof(FSHApiConventions.Search))]
    public Task<string> DeleteRandomAsync()
    {
        return Mediator.Send(new DeleteRandomBrandRequest());
    }
}