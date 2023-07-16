using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.EntityCodes;

public class EntityCodeByIdSpec : Specification<EntityCode, EntityCodeDetailsDto>, ISingleResultSpecification<EntityCode>
{
    public EntityCodeByIdSpec(DefaultIdType id) =>
        Query
            .Where(e => e.Id == id);
}

public class EntityCodeByCodeSpec : Specification<EntityCode>, ISingleResultSpecification<EntityCode>
{
    public EntityCodeByCodeSpec(string code) =>
        Query
            .Where(e => e.Code == code);
}

public class EntityCodeByNameSpec : Specification<EntityCode>, ISingleResultSpecification<EntityCode>
{
    public EntityCodeByNameSpec(string name) =>
        Query
            .Where(e => e.Name == name);
}