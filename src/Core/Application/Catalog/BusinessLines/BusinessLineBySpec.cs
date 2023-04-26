namespace FSH.WebApi.Application.Catalog.BusinessLines;

public class BusinessLineByIdSpec : Specification<BusinessLine, BusinessLineDetailsDto>, ISingleResultSpecification<BusinessLine>
{
    public BusinessLineByIdSpec(Guid id) =>
        Query
            .Where(e => e.Id == id);
}

public class BusinessLineByCodeSpec : Specification<BusinessLine>, ISingleResultSpecification<BusinessLine>
{
    public BusinessLineByCodeSpec(string code) =>
        Query
            .Where(e => e.Code == code);
}

public class BusinessLineByNameSpec : Specification<BusinessLine>, ISingleResultSpecification<BusinessLine>
{
    public BusinessLineByNameSpec(string name) =>
        Query
            .Where(e => e.Name == name);
}