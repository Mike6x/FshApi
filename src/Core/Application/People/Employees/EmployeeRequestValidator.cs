using FSH.WebApi.Domain.People;

namespace FSH.WebApi.Application.People.Employees;

public class CreateEmployeeRequestValidator : CustomValidator<CreateEmployeeRequest>
{
    public CreateEmployeeRequestValidator(IReadRepository<Employee> entityRepo, IStringLocalizer<CreateEmployeeRequestValidator> T)
    {
        RuleFor(e => e.Code).Cascade(CascadeMode.Stop)
        .NotEmpty()
        .MaximumLength(75)
        .MustAsync(async (code, ct) => await entityRepo.FirstOrDefaultAsync(new EmployeeByCodeSpec(code), ct) is null)
            .WithMessage((_, code) => T["Employee with code: {0} already exists.", code]);

        RuleFor(e => e.FirstName)
            .NotEmpty()
            .MaximumLength(75);

        RuleFor(e => e.LastName)
            .NotEmpty()
            .MaximumLength(75);

        RuleFor(e => e.Email).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress()
                 .WithMessage(T["Invalid Email Address."])
            .MustAsync(async (email, ct) => await entityRepo.FirstOrDefaultAsync(new EmployeeByEmailSpec(email), ct) is null)
                .WithMessage((_, email) => T["Employee with email {0} already exists.", email]);

        RuleFor(e => e.PhoneNumber)
            .NotEmpty()
                .WithMessage(" The Phone is required");

        RuleFor(e => e.TitleId)
            .NotEmpty()
                .WithMessage(" The Title is required");

        RuleFor(e => e.BusinessUnitId)
            .NotEmpty()
                .WithMessage(" The Business Unit is required");
    }
}

public class UpdateEmployeeRequestValidator : CustomValidator<UpdateEmployeeRequest>
{
    public UpdateEmployeeRequestValidator(IReadRepository<Employee> entityRepo, IStringLocalizer<UpdateEmployeeRequestValidator> T)
    {
        RuleFor(e => e.Code).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, code, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new EmployeeByCodeSpec(code), ct)
                        is not Employee existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, code) => T["Employee {0} already Exists.", code]);

        RuleFor(e => e.FirstName)
            .NotEmpty()
            .MaximumLength(75);

        RuleFor(e => e.LastName)
            .NotEmpty()
            .MaximumLength(75);

        RuleFor(e => e.Email).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress()
                 .WithMessage(T["Invalid Email Address."])
            .MustAsync(async (entity, email, ct) =>
                await entityRepo.FirstOrDefaultAsync(new EmployeeByEmailSpec(email), ct)
                    is not Employee existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, email) => T["Employee with email {0} already Exists.", email]);

        RuleFor(e => e.PhoneNumber)
            .NotEmpty()
                .WithMessage(" The Phone is required");

        RuleFor(e => e.TitleId)
            .NotEmpty()
                .WithMessage(" The Title is required");

        RuleFor(e => e.BusinessUnitId)
            .NotEmpty()
                .WithMessage(" The Business Unit is required");
    }
}