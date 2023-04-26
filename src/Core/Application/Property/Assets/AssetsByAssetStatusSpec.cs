using FSH.WebApi.Domain.Property;

namespace FSH.WebApi.Application.Property.Assets;

public class AssetsByAssetStatusSpec : Specification<Asset>
{
    public AssetsByAssetStatusSpec(DefaultIdType fatherId) =>
        Query.Where(e => e.QualityStatusId == fatherId || e.UsingStatusId == fatherId);
}