using FSH.WebApi.Application.Leave.LeaveAllocations;
using FSH.WebApi.Application.Settings.Dimensions;
using FSH.WebApi.Domain.Leave;
using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Leave.LeaveApplications;

public class SubmitLeaveApplicationRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }

    public DefaultIdType EmployeeId { get; set; }
    public DefaultIdType LeaveTypeId { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public DayOffType FirstLeaveDay { get; set; }
    public DayOffType LastLeaveDay { get; set; }
    public DateTime? RequestOn { get; set; }
    public string? RequestRemarks { get; set; }
    public RequestStatus Status { get; set; }
    public DefaultIdType ApproverId { get; set; }
    public DateTime? ApprovedOn { get; set; }
    public string? ApproverRemarks { get; set; }
    public double? NumberOfValidDays { get; set; }
    public double? NumberOfOnHandDays { get; set; }
}

public class SubmitRequestHandler : IRequestHandler<SubmitLeaveApplicationRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<LeaveApplication> _repository;
    private readonly IRepositoryWithEvents<LeaveAllocation> _leaveAllocationRepo;
    private readonly IReadRepository<Dimension> _dimensionRepo;
    private readonly IStringLocalizer _t;

    public SubmitRequestHandler(
        IRepositoryWithEvents<LeaveApplication> repository,
        IRepositoryWithEvents<LeaveAllocation> leaveAllocationRepo,
        IReadRepository<Dimension> dimensionRepo,
        IStringLocalizer<SubmitRequestHandler> localizer) =>
            (_repository, _leaveAllocationRepo, _dimensionRepo, _t) = (repository, leaveAllocationRepo, dimensionRepo, localizer);

    public async Task<DefaultIdType> Handle(SubmitLeaveApplicationRequest request, CancellationToken cancellationToken)
    {
        if (request.Status != RequestStatus.Edited) request.Status = RequestStatus.Submited;

        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);

        if (entity.Status == RequestStatus.Edited)
        {
            var leaveType = await _dimensionRepo.FirstOrDefaultAsync(new DimensionByIdSpec(request.LeaveTypeId), cancellationToken)
                ?? throw new NotFoundException(_t["LeaveType with ID: {0} not found.", request.LeaveTypeId]);

            request.FromDate ??= entity.FromDate;
            request.ToDate ??= entity.ToDate;
            if (request.ToDate < request.FromDate) request.ToDate = request.FromDate;

            double offTime = LeaveApplicationHelper.DayOffCalculate((DateTime)request.FromDate, (DateTime)request.ToDate, request.FirstLeaveDay, request.LastLeaveDay);

            if (offTime > leaveType.Value) throw new ConflictException(_t["Requested time is over limitation."]);

            double numberOfOnHandDays = leaveType.Value - offTime;
            double numberOfValidDays = leaveType.Value;
            Guid? empAllocationId = null;

            if (request.Status == RequestStatus.Submited && leaveType.FatherId != null && leaveType.FatherId != Guid.Empty)
            {

                var empAllocation = await _leaveAllocationRepo.FirstOrDefaultAsync(
                        new LeaveAllocationsBySpecs(true, null, entity.EmployeeId, (DefaultIdType)leaveType.FatherId!, false), cancellationToken)
                    ?? throw new ConflictException(_t["Employee with ID: {0} do not have allocation room for this kind of request.", request.EmployeeId]);

                if (offTime > empAllocation!.NumberOfOnHandDays)
                {
                    throw new ConflictException(_t["Employee with ID: {0} do not have enought allocation room", request.EmployeeId]);
                }

                empAllocationId = empAllocation!.Id;
                numberOfValidDays = empAllocation!.NumberOfValidDays;
                numberOfOnHandDays = empAllocation!.NumberOfOnHandDays - offTime;

                empAllocation.Update(null, null, null, null, null, null, null, null, null, null, null, numberOfOnHandDays, null);
                await _leaveAllocationRepo.UpdateAsync(empAllocation, cancellationToken);
            }

            entity.Update(
                null,
                request.LeaveTypeId,
                request.FromDate,
                request.ToDate,
                request.FirstLeaveDay,
                request.LastLeaveDay,
                request.RequestOn,
                request.RequestRemarks,
                request.Status,
                request.ApproverId,
                request.ApprovedOn,
                request.ApproverRemarks,
                empAllocationId,
                numberOfValidDays,
                numberOfOnHandDays);

            await _repository.UpdateAsync(entity, cancellationToken);

            return request.Id;
        }
        else
        {
            throw new ConflictException(_t["The leave request can not be changed because it have submited."]);
        }
    }
}