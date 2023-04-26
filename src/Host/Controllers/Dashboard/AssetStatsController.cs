using FSH.WebApi.Application.Dashboard;

namespace FSH.WebApi.Host.Controllers.Dashboard;

public class AssetStatsController : VersionedApiController
{
    [HttpGet]
    [MustHavePermission(FSHAction.View, FSHResource.AssetStats)]
    [OpenApiOperation("Get Asset Statistics for the dashboard.", "")]
    public Task<AssetStatsDto> GetAsync()
    {
        return Mediator.Send(new GetAssetStatsRequest());
    }
}