using FSH.WebApi.Domain.Purchase;

namespace FSH.WebApi.Application.Purchase.Vendors;

public class GetVendorRequest : IRequest<VendorDetailsDto>
{
    public DefaultIdType Id { get; set; }
    public GetVendorRequest(DefaultIdType id) => Id = id;
}

public class GetVendorRequestHandler : IRequestHandler<GetVendorRequest, VendorDetailsDto>
{
    private readonly IRepository<Vendor> _repository;
    private readonly IStringLocalizer _t;

    public GetVendorRequestHandler(IRepository<Vendor> repository, IStringLocalizer<GetVendorRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    [Obsolete]
    public async Task<VendorDetailsDto> Handle(GetVendorRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<Vendor, VendorDetailsDto>)new VendorByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);
}