using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.Dimensions;

public class DimensionByIdSpec : Specification<Dimension, DimensionDetailsDto>, ISingleResultSpecification<Dimension>
{
    public DimensionByIdSpec(DefaultIdType id) =>
        Query
            .Where(e => e.Id == id);
}

public class DimensionByCodeSpec : Specification<Dimension>, ISingleResultSpecification<Dimension>
{
    public DimensionByCodeSpec(string code) =>
        Query
            .Where(e => e.Code == code);
}

public class DimensionByNameSpec : Specification<Dimension>, ISingleResultSpecification<Dimension>
{
    public DimensionByNameSpec(string name) =>
        Query
            .Where(e => e.Name == name);
}

public class DimensionByTypeSpec : Specification<Dimension>, ISingleResultSpecification<Dimension>
{
    public DimensionByTypeSpec(string type) =>
        Query
            .Where(e => e.Type == type);
}