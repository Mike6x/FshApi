using FSH.WebApi.Application.Dashboard;

namespace FSH.WebApi.Host.Controllers.Dashboard;

public class EmployeeStatsController : VersionedApiController
{
    [HttpGet]
    [MustHavePermission(FSHAction.View, FSHResource.EmployeeStats)]
    [OpenApiOperation("Get Employee Statistics for the dashboard.", "")]
    public Task<EmployeeStatsDto> GetAsync()
    {
        return Mediator.Send(new GetEmployeeStatsRequest());
    }
}