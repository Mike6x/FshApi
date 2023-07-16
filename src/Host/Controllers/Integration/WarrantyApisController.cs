using FSH.WebApi.Application.Integration;
using FSH.WebApi.Domain.Integration;

namespace FSH.WebApi.Host.Controllers.Integration;

public class WarrantyApisController : VersionedApiController
{
    [HttpPost("GetSerialList")]
    [TenantIdHeader]
    [AllowAnonymous]
    [OpenApiOperation("Get ChatMessage between two users.", "")]
    public Task<List<ApiSerial>> GetListAsync(GetSerialListRequest request)
    {
        return Mediator.Send(request);
    }
}
