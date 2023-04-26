using FSH.WebApi.Domain.Property;

namespace FSH.WebApi.Application.Property.AssetStatuses;

public class AssetStatusByIdSpec : Specification<AssetStatus, AssetStatusDetailsDto>, ISingleResultSpecification<AssetStatus>
{
    public AssetStatusByIdSpec(DefaultIdType id) =>
        Query
            .Where(e => e.Id == id);
}

public class AssetStatusByCodeSpec : Specification<AssetStatus>, ISingleResultSpecification<AssetStatus>
{
    public AssetStatusByCodeSpec(string code) =>
        Query
            .Where(e => e.Code == code);
}

public class AssetStatusByNameSpec : Specification<AssetStatus>, ISingleResultSpecification<AssetStatus>
{
    public AssetStatusByNameSpec(string name) =>
        Query
            .Where(e => e.Name == name);
}