using FSH.WebApi.Domain.Purchase;

namespace FSH.WebApi.Application.Purchase.Vendors;

public class DeleteVendorRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }

    public DeleteVendorRequest(DefaultIdType id) => Id = id;
}

public class DeleteVendorRequestHandler : IRequestHandler<DeleteVendorRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Vendor> _repository;

   // private readonly IReadRepository<Asset> _childRepository;
    private readonly IStringLocalizer _t;

    public DeleteVendorRequestHandler(IRepositoryWithEvents<Vendor> repository, IStringLocalizer<DeleteVendorRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<DefaultIdType> Handle(DeleteVendorRequest request, CancellationToken cancellationToken)
    {
        // if (await _childRepository.AnyAsync(new EmployeesByVendorSpec(request.Id), cancellationToken))
        // {
        //    throw new ConflictException(_t["Vendor cannot be deleted as it's being used."]);
        // }

        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity ?? throw new NotFoundException(_t["Vendor {0} Not Found."]);

        await _repository.DeleteAsync(entity, cancellationToken);

        return request.Id;
    }
}
