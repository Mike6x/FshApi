using FSH.WebApi.Application.Dashboard;

namespace FSH.WebApi.Host.Controllers.Dashboard;

public class UserStatsController : VersionedApiController
{
    [HttpGet]
    [MustHavePermission(FSHAction.View, FSHResource.UserStats)]
    [OpenApiOperation("Get statistics for the user dashboard.", "")]
    public Task<UserStatsDto> GetAsync()
    {
        return Mediator.Send(new GetUserStatsRequest());
    }
}