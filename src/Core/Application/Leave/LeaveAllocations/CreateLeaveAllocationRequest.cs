using FSH.WebApi.Domain.Leave;

namespace FSH.WebApi.Application.Leave.LeaveAllocations;

public class CreateLeaveAllocationRequest : IRequest<Guid>
{
    public bool IsActive { get; set; } = true;
    public int Period { get; set; } = DateTime.Now.Year;
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }

    public DefaultIdType EmployeeId { get; set; }
    public DefaultIdType LeaveAllocationTypeId { get; set; }

    public int NumberOfAnnualDays { get; set; }
    public int NumberOfExtraDays { get; set; }
    public double NumberOfCarryOverDays { get; set; }
    public double NumberOfCompensationDays { get; set; }

    // public double NumberOfValidDays { get; set; }
    // public double NumberOfOnHandDays { get; set; }
    // public bool IsLocked { get; set; }
}

public class CreateRequestHandler(IRepositoryWithEvents<LeaveAllocation> repository, IStringLocalizer<DeleteRequestHandler> localizer) : IRequestHandler<CreateLeaveAllocationRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<LeaveAllocation> _repository = repository;
    private readonly IStringLocalizer _t = localizer;
    public async Task<Guid> Handle(CreateLeaveAllocationRequest request, CancellationToken cancellationToken)
    {
        if (await _repository.AnyAsync(new LeaveAllocationsBySpecs(null, request.Period, request.EmployeeId, request.LeaveAllocationTypeId, null), cancellationToken))
        {
            throw new ConflictException(_t["Entity for employee {0} have existed.", request.EmployeeId]);
        }

        var entity = new LeaveAllocation(
            request.IsActive,
            request.Period,
            request.FromDate == DateTime.MinValue ? DateTime.Now : request.FromDate,
            request.ToDate == DateTime.MinValue ? DateTime.Now.AddDays(365) : request.FromDate,
            request.EmployeeId,
            request.LeaveAllocationTypeId,
            request.NumberOfAnnualDays,
            request.NumberOfExtraDays,
            request.NumberOfCarryOverDays,
            request.NumberOfCompensationDays,
            request.NumberOfAnnualDays + request.NumberOfExtraDays + request.NumberOfCarryOverDays + request.NumberOfCompensationDays,
            request.NumberOfAnnualDays + request.NumberOfExtraDays + request.NumberOfCarryOverDays + request.NumberOfCompensationDays,
            false);

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}
