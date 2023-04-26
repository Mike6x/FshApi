using FSH.WebApi.Application.Identity.Users;
using FSH.WebApi.Application.People.Employees;
using FSH.WebApi.Domain.People;
using FSH.WebApi.Domain.Property;

namespace FSH.WebApi.Application.Property.Assets;
internal class AssetDeliveryDto
{
    public UserDetailsDto DeliveryMan { get; set; } = default!;
    public EmployeeDto ReceiverMan { get; set; } = default!;
    public List<Asset> Assets { get; set; } = default!;
}