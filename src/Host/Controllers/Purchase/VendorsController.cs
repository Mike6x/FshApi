using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Application.Purchase.Vendors;

namespace FSH.WebApi.Host.Controllers.People;

public class VendorsController : VersionedApiController
{
    private readonly IExcelReader _excelReader;
    public VendorsController(IExcelReader excelReader)
    {
        _excelReader = excelReader;
    }

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Vendors)]
    [OpenApiOperation("Search Vendors using available filters.", "")]
    public Task<PaginationResponse<VendorDto>> SearchAsync(SearchVendorsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Vendors)]
    [OpenApiOperation("Get Vendor details.", "")]
    public Task<VendorDetailsDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new GetVendorRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Vendors)]
    [OpenApiOperation("Create a new Vendor.", "")]
    public Task<DefaultIdType> CreateAsync(CreateVendorRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Vendors)]
    [OpenApiOperation("Update a Vendor.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(UpdateVendorRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Vendors)]
    [OpenApiOperation("Delete a Vendor.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new DeleteVendorRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.Vendors)]
    [OpenApiOperation("Export a Vendors.", "")]
    public async Task<FileResult> ExportAsync(ExportVendorsRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "VendorExports");
    }

    [HttpPost("import")]
    [MustHavePermission(FSHAction.Import, FSHResource.Vendors)]
    [OpenApiOperation("Import a Vendors.", "")]
    public async Task<ActionResult<int>> ImportAsync(ImportVendorsRequest request)
    {
        return Ok(await Mediator.Send(request));
    }

}