using FSH.WebApi.Domain.Organization;

namespace FSH.WebApi.Application.Organization.Departments;

public class CreateDepartmentRequestValidator : CustomValidator<CreateDepartmentRequest>
{
    public CreateDepartmentRequestValidator(IReadRepository<Department> entityRepo, IReadRepository<BusinessUnit> fatherRepo, IStringLocalizer<CreateDepartmentRequestValidator> t)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (code, ct) => await entityRepo.FirstOrDefaultAsync(new DepartmentByCodeSpec(code), ct) is null)
                .WithMessage((_, code) => t["Department with Code{0} already exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await entityRepo.FirstOrDefaultAsync(new DepartmentByNameSpec(name), ct) is null)
                .WithMessage((_, name) => t["Department with Name {0} already exists.", name]);

        RuleFor(e => e.BusinessUnitId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await fatherRepo.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => t["BusinessUnit {0} Not Found.", id]);

    }
}

public class UpdateDepartmentRequestValidator : CustomValidator<UpdateDepartmentRequest>
{
    public UpdateDepartmentRequestValidator(IReadRepository<Department> entityRepo, IReadRepository<BusinessUnit> fatherRepo, IStringLocalizer<UpdateDepartmentRequestValidator> t)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, code, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new DepartmentByCodeSpec(code), ct)
                        is not Department existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, code) => t["Department {0} already Exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, name, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new DepartmentByNameSpec(name), ct)
                        is not Department existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, name) => t["Department {0} already Exists.", name]);

        RuleFor(e => e.BusinessUnitId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await fatherRepo.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => t["BusinessUnit {0} Not Found.", id]);
    }
}