using FSH.WebApi.Domain.Integration;
using System.ComponentModel;

namespace FSH.WebApi.Application.Integration;
public interface IWarrantyApi : IScopedService
{
    [DisplayName("Get Item Serial list job from Netsuite")]
    Task<List<ApiSerial>> GetSerialsAsync(Dictionary<string, string>? parameters);

    Task<int> GetAndUpdateSerialsAsync(Dictionary<string, string>? parameters);
}
