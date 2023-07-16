namespace FSH.WebApi.Application.Leave.LeaveAllocations;

public class CreateLeaveAllocationRequestValidator : CustomValidator<CreateLeaveAllocationRequest>
{
    public CreateLeaveAllocationRequestValidator()
    {
        RuleFor(e => e.EmployeeId).NotEmpty().NotEqual(Guid.Empty);

        RuleFor(e => e.LeaveAllocationTypeId)
            .NotEmpty()
            .NotEqual(Guid.Empty);

        RuleFor(e => e.Period)
            .NotEmpty()
            .GreaterThanOrEqualTo(1);
    }
}

public class UpdateLeaveAllocationRequestValidator : CustomValidator<UpdateLeaveAllocationRequest>
{
    public UpdateLeaveAllocationRequestValidator()
    {
        RuleFor(e => e.EmployeeId).NotEmpty().NotEqual(Guid.Empty);

        RuleFor(e => e.LeaveAllocationTypeId)
            .NotEmpty()
            .NotEqual(Guid.Empty);

        RuleFor(e => e.Period)
            .NotEmpty()
            .GreaterThanOrEqualTo(1);
    }
}