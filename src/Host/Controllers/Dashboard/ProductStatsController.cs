using FSH.WebApi.Application.Dashboard;

namespace FSH.WebApi.Host.Controllers.Dashboard;

public class ProductStatsController : VersionedApiController
{
    [HttpGet]
    [MustHavePermission(FSHAction.View, FSHResource.ProductStats)]
    [OpenApiOperation("Get product statistics for the dashboard.", "")]
    public Task<ProductStatsDto> GetAsync()
    {
        return Mediator.Send(new GetProductStatsRequest());
    }
}