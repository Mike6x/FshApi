using FSH.WebApi.Application.Leave.LeaveApplications;
using FSH.WebApi.Domain.Leave;

namespace FSH.WebApi.Application.Leave.LeaveAllocations;

public class DeleteLeaveAllocationRequest(Guid id) : IRequest<Guid>
{
    public Guid Id { get; set; } = id;
}

public class DeleteRequestHandler : IRequestHandler<DeleteLeaveAllocationRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<LeaveAllocation> _repository;
    private readonly IReadRepository<LeaveApplication> _leaveApplicationRepo;
    private readonly IStringLocalizer _t;
    public DeleteRequestHandler(
        IRepositoryWithEvents<LeaveAllocation> repository,
        IReadRepository<LeaveApplication> leaveApplicationRepo,
        IStringLocalizer<DeleteRequestHandler> localizer) =>
        (_repository, _leaveApplicationRepo, _t) = (repository, leaveApplicationRepo, localizer);

    public async Task<Guid> Handle(DeleteLeaveAllocationRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity ?? throw new NotFoundException(_t["Entity {0} Not Found."]);
        if (entity.IsLocked) throw new ConflictException(_t["Entity {0} can not be deleted because it's locked."]);

        if (await _leaveApplicationRepo.AnyAsync(new LeaveApplicationsBySpecs(entity.Period, entity.EmployeeId, entity.LeaveAllocationTypeId), cancellationToken))
        {
            throw new ConflictException(_t["LeaveAllocation cannot be deleted as it's being used."]);
        }

        await _repository.DeleteAsync(entity, cancellationToken);

        return request.Id;
    }
}