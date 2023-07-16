namespace FSH.WebApi.Application.Leave.LeaveApplications;
public class CreateLeaveApplicationRequestValidator : CustomValidator<CreateLeaveApplicationRequest>
{
    public CreateLeaveApplicationRequestValidator()
    {
        RuleFor(e => e.EmployeeId).NotEmpty().NotEqual(Guid.Empty);

        RuleFor(e => e.LeaveTypeId)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}

public class UpdateLeaveApplicationRequestValidator : CustomValidator<UpdateLeaveApplicationRequest>
{
    public UpdateLeaveApplicationRequestValidator()
    {
        RuleFor(e => e.EmployeeId).NotEmpty().NotEqual(Guid.Empty);

        RuleFor(e => e.LeaveTypeId)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}