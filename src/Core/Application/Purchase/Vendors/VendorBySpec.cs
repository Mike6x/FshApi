using FSH.WebApi.Domain.Purchase;

namespace FSH.WebApi.Application.Purchase.Vendors;

public class VendorByIdSpec : Specification<Vendor, VendorDetailsDto>, ISingleResultSpecification<Vendor>
{
    public VendorByIdSpec(DefaultIdType id) =>
        Query
            .Where(e => e.Id == id);
}

public class VendorByCodeSpec : Specification<Vendor>, ISingleResultSpecification<Vendor>
{
    public VendorByCodeSpec(string code) =>
        Query
            .Where(e => e.Code == code);
}

public class VendorByNameSpec : Specification<Vendor>, ISingleResultSpecification<Vendor>
{
    public VendorByNameSpec(string name) =>
        Query
            .Where(e => e.Name == name);
}