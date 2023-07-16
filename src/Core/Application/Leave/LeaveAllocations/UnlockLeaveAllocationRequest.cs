using FSH.WebApi.Domain.Leave;

namespace FSH.WebApi.Application.Leave.LeaveAllocations;

public class UnlockLeaveAllocationRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }
}

public class UnlockRequestHandler : IRequestHandler<UnlockLeaveAllocationRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<LeaveAllocation> _repository;
    private readonly IStringLocalizer _t;

    public UnlockRequestHandler(
        IRepositoryWithEvents<LeaveAllocation> repository,
        IStringLocalizer<UnlockRequestHandler> localizer) =>
            (_repository, _t) = (repository, localizer);

    public async Task<DefaultIdType> Handle(UnlockLeaveAllocationRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);

        if (entity.IsLocked) entity.Lock(false);
        else entity.Lock(true);

        await _repository.UpdateAsync(entity, cancellationToken);

        return request.Id;
    }
}
