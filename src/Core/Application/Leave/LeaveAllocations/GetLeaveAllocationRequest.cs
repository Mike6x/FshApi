using FSH.WebApi.Domain.Leave;

namespace FSH.WebApi.Application.Leave.LeaveAllocations;

public class GetLeaveAllocationRequest(Guid id) : IRequest<LeaveAllocationDetailsDto>
{
    public Guid Id { get; set; } = id;
}

public class GetRequestHandler : IRequestHandler<GetLeaveAllocationRequest, LeaveAllocationDetailsDto>
{
    private readonly IRepository<LeaveAllocation> _repository;
    private readonly IStringLocalizer _t;

    public GetRequestHandler(IRepository<LeaveAllocation> repository, IStringLocalizer<GetRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<LeaveAllocationDetailsDto> Handle(GetLeaveAllocationRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<LeaveAllocation, LeaveAllocationDetailsDto>)new LeaveAllocationByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);
}
