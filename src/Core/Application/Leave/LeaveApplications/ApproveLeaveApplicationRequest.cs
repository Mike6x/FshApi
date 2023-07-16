using FSH.WebApi.Application.Leave.LeaveAllocations;
using FSH.WebApi.Application.People.Employees;
using FSH.WebApi.Domain.Leave;
using FSH.WebApi.Domain.People;
using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Leave.LeaveApplications;

public class ApproveLeaveApplicationRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }

    public string ApproverLogonId { get; set; } = default!;
    public string? ApproverRemarks { get; set; }
    public bool IsApproved { get; set; }
}

public class ApproveRequestHandler : IRequestHandler<ApproveLeaveApplicationRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<LeaveApplication> _repository;
    private readonly IRepositoryWithEvents<LeaveAllocation> _leaveAllocationRepo;
    private readonly IReadRepository<Dimension> _dimensionRepo;
    private readonly IReadRepository<Employee> _employeeRepo;
    private readonly IStringLocalizer _t;

    public ApproveRequestHandler(
        IRepositoryWithEvents<LeaveApplication> repository,
        IRepositoryWithEvents<LeaveAllocation> leaveAllocationRepo,
        IReadRepository<Dimension> dimensionRepo,
        IReadRepository<Employee> employeeRepo,
        IStringLocalizer<UpdateRequestHandler> localizer) =>
            (_repository, _leaveAllocationRepo, _dimensionRepo, _employeeRepo, _t) = (repository, leaveAllocationRepo, dimensionRepo, employeeRepo, localizer);

    public async Task<DefaultIdType> Handle(ApproveLeaveApplicationRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);

        var approver = await _employeeRepo.FirstOrDefaultAsync(new EmployeeByUserIdSpec(request.ApproverLogonId), cancellationToken)
            ?? throw new NotFoundException(_t["Approver with logon Id {0} Not Found.", request.ApproverLogonId]);

        double offTime = LeaveApplicationHelper.DayOffCalculate(entity.FromDate, entity.ToDate, entity.FirstLeaveDay, entity.LastLeaveDay);

        RequestStatus nextStatus = RequestStatus.Submited;
        if (entity.Status == RequestStatus.Submited) nextStatus = request.IsApproved ? RequestStatus.Approved : RequestStatus.Rejected;
        if (entity.Status == RequestStatus.Revoked || entity.Status == RequestStatus.Approved)
            nextStatus = request.IsApproved ? RequestStatus.RevokeApproved : RequestStatus.Approved;
        double? numberOfValidDays = null;
        double? numberOfOnHandDays = null;

        if (entity.LeaveAllocationId != null && entity.LeaveAllocationId != Guid.Empty)
        {
            var empAllocation = await _leaveAllocationRepo.GetByIdAsync((DefaultIdType)entity.LeaveAllocationId!, cancellationToken)
                ?? throw new ConflictException(_t["Not found leave allocation for Employee with ID: {0}.", entity.EmployeeId]);

            if (entity.Status == RequestStatus.Submited)
            {
                if (request.IsApproved && offTime > empAllocation!.NumberOfValidDays)
                    {
                        throw new ConflictException(_t["Not enought allocation room for Employee with ID: {0} request", entity.EmployeeId]);
                    }

                numberOfValidDays = request.IsApproved ? empAllocation.NumberOfValidDays - offTime : null;
                numberOfOnHandDays = request.IsApproved ? null : empAllocation.NumberOfOnHandDays + offTime;
            }

            if (entity.Status == RequestStatus.Revoked || entity.Status == RequestStatus.Approved)
            {
                numberOfValidDays = request.IsApproved ? empAllocation.NumberOfValidDays + offTime : null;
                numberOfOnHandDays = request.IsApproved ? empAllocation.NumberOfOnHandDays + offTime : null;
            }

            empAllocation.Update(null, null, null, null, null, null, null, null, null, null, numberOfValidDays, numberOfOnHandDays, null);
            await _leaveAllocationRepo.UpdateAsync(empAllocation, cancellationToken);
        }

        entity.Update(
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            nextStatus,
            approver.Id,
            DateTime.UtcNow,
            request.ApproverRemarks,
            null,
            null,
            null);

        await _repository.UpdateAsync(entity, cancellationToken);

        return request.Id;
    }
}