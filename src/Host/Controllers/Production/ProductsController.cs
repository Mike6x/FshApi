﻿using FSH.WebApi.Application.Production.Products;

namespace FSH.WebApi.Host.Controllers.Production;

public class ProductsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Products)]
    [OpenApiOperation("Search products using available filters.", "")]
    public Task<PaginationResponse<ProductDto>> SearchAsync(SearchProductsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Products)]
    [OpenApiOperation("Get product details.", "")]
    public Task<ProductDetailsDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new GetProductRequest(id));
    }

    [HttpGet("dapper")]
    [MustHavePermission(FSHAction.View, FSHResource.Products)]
    [OpenApiOperation("Get product details via dapper.", "")]
    public Task<ProductDto> GetDapperAsync(DefaultIdType id)
    {
        return Mediator.Send(new GetProductViaDapperRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Products)]
    [OpenApiOperation("Create a new product.", "")]
    public Task<DefaultIdType> CreateAsync(CreateProductRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Products)]
    [OpenApiOperation("Update a product.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(UpdateProductRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Products)]
    [OpenApiOperation("Delete a product.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new DeleteProductRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.Products)]
    [OpenApiOperation("Export a products.", "")]
    public async Task<FileResult> ExportAsync(ExportProductsRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "ProductExports");
    }

    [HttpPost("import")]
    [MustHavePermission(FSHAction.Import, FSHResource.Products)]
    [OpenApiOperation("Import a Products.", "")]
    public async Task<ActionResult<int>> ImportAsync(ImportProductsRequest request)
    {
        return Ok(await Mediator.Send(request));
    }
}