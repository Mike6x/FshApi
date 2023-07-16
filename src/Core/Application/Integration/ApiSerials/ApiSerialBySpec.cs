using FSH.WebApi.Domain.Integration;

namespace FSH.WebApi.Application.Integration.ApiSerials;

public class ApiSerialByIdSpec : Specification<ApiSerial, ApiSerialDetailsDto>, ISingleResultSpecification<ApiSerial>
{
    public ApiSerialByIdSpec(DefaultIdType id) =>
        Query
            .Where(e => e.Id == id);
}

public class ApiSerialByCodeSpec : Specification<ApiSerial>, ISingleResultSpecification<ApiSerial>
{
    public ApiSerialByCodeSpec(string itemCode) =>
        Query
            .Where(e => e.ItemCode == itemCode);
}

public class ApiSerialByNameSpec : Specification<ApiSerial>, ISingleResultSpecification<ApiSerial>
{
    public ApiSerialByNameSpec(string itemName) =>
        Query
            .Where(e => e.ItemName == itemName);
}

public class ApiSerialBySerial : Specification<ApiSerial>, ISingleResultSpecification<ApiSerial>
{
    public ApiSerialBySerial(string itemSerial) =>
        Query
            .Where(e => e.ItemSerial == itemSerial);
}