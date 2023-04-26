using FSH.WebApi.Domain.Purchase;

namespace FSH.WebApi.Application.Purchase.Vendors;

public class CreateVendorRequest : IRequest<DefaultIdType>
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? ContactPerson { get; set; }

    public string? TaxCode { get; set; }
}

public class CreateVendorRequestHandler : IRequestHandler<CreateVendorRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Vendor> _repository;

    public CreateVendorRequestHandler(IRepositoryWithEvents<Vendor> repository) => _repository = repository;

    public async Task<DefaultIdType> Handle(CreateVendorRequest request, CancellationToken cancellationToken)
    {
        var entity = new Vendor(
                request.Code,
                request.Name,
                request.Description ?? string.Empty,
                request.IsActive,
                request.Phone ?? string.Empty,
                request.Email ?? string.Empty,
                request.Address ?? string.Empty,
                request.ContactPerson ?? string.Empty,
                request.TaxCode ?? string.Empty);

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}
