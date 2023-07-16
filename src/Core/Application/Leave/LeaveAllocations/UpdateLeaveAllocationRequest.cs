using FSH.WebApi.Domain.Leave;

namespace FSH.WebApi.Application.Leave.LeaveAllocations;

public class UpdateLeaveAllocationRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }

    public bool? IsActive { get; set; }
    public int? Period { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }

    public DefaultIdType EmployeeId { get; set; }
    public DefaultIdType LeaveAllocationTypeId { get; set; }

    public int? NumberOfAnnualDays { get; set; }
    public int? NumberOfExtraDays { get; set; }
    public double? NumberOfCarryOverDays { get; set; }
    public double? NumberOfCompensationDays { get; set; }
    public double? NumberOfValidDays { get; set; }
    public double? NumberOfOnHandDays { get; set; }

    public bool? IsLocked { get; set; }
}

public class UpdateRequestHandler : IRequestHandler<UpdateLeaveAllocationRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<LeaveAllocation> _repository;
    private readonly IStringLocalizer _t;

    public UpdateRequestHandler(
        IRepositoryWithEvents<LeaveAllocation> repository,
        IStringLocalizer<UpdateRequestHandler> localizer) =>
            (_repository, _t) = (repository, localizer);

    public async Task<DefaultIdType> Handle(UpdateLeaveAllocationRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);

        if (entity.IsLocked) throw new ConflictException(_t["Entity {0} can not update because it's locked.", request.Id]);

        entity.Update(
            request.IsActive,
            request.Period,
            request.FromDate,
            request.ToDate,
            request.EmployeeId,
            request.LeaveAllocationTypeId,
            request.NumberOfAnnualDays,
            request.NumberOfExtraDays,
            request.NumberOfCarryOverDays,
            request.NumberOfCompensationDays,
            request.NumberOfValidDays,
            request.NumberOfOnHandDays,
            request.IsLocked);

        await _repository.UpdateAsync(entity, cancellationToken);

        return request.Id;
    }
}