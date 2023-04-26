using FSH.WebApi.Domain.Property;

namespace FSH.WebApi.Application.Property.AssetHistorys;

public class AssetHistoryByIdSpec : Specification<AssetHistory, AssetHistoryDetailsDto>, ISingleResultSpecification<AssetHistory>
{
    public AssetHistoryByIdSpec(DefaultIdType id) =>
        Query
            .Where(e => e.Id == id);
}

public class AssetHistoryByCodeSpec : Specification<AssetHistory>, ISingleResultSpecification<AssetHistory>
{
    public AssetHistoryByCodeSpec(DefaultIdType assetId) =>
        Query
            .Where(e => e.AssetId == assetId);
}

public class AssetHistoryByNameSpec : Specification<AssetHistory>, ISingleResultSpecification<AssetHistory>
{
    public AssetHistoryByNameSpec(string assetName) =>
        Query
            .Where(e => e.Asset.Name == assetName);
}