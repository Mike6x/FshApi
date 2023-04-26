using FSH.WebApi.Application.Property.AssetStatuses;
using FSH.WebApi.Domain.Property;

namespace FSH.WebApi.Application.Property.Assets;

public class AssetByIdSpec : Specification<Asset, AssetDetailsDto>, ISingleResultSpecification<Asset>
{
    public AssetByIdSpec(DefaultIdType id) =>
        Query
            .Where(e => e.Id == id);
}

public class AssetByCodeSpec : Specification<Asset>, ISingleResultSpecification<Asset>
{
    public AssetByCodeSpec(string code) =>
        Query
            .Where(e => e.Code == code);
}

public class AssetByNameSpec : Specification<Asset>, ISingleResultSpecification<Asset>
{
    public AssetByNameSpec(string name) =>
        Query
            .Where(e => e.Name == name);
}