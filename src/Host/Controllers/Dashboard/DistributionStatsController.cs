using FSH.WebApi.Application.Dashboard;

namespace FSH.WebApi.Host.Controllers.Dashboard;

public class DistributionStatsController : VersionedApiController
{
    [HttpGet]
    [MustHavePermission(FSHAction.View, FSHResource.DistributionStats)]
    [OpenApiOperation("Get Distribution Statistics for the dashboard.", "")]
    public Task<DistributionStatsDto> GetAsync()
    {
        return Mediator.Send(new GetDistributionStatsRequest());
    }
}