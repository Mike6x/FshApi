using FSH.WebApi.Domain.Organization;
using FSH.WebApi.Domain.People;

namespace FSH.WebApi.Application.Organization.SubDepartments;

public class CreateSubDepartmentRequestValidator : CustomValidator<CreateSubDepartmentRequest>
{
    public CreateSubDepartmentRequestValidator(IReadRepository<SubDepartment> entityRepo, IReadRepository<Department> fatherRepo, IStringLocalizer<CreateSubDepartmentRequestValidator> t)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (code, ct) => await entityRepo.FirstOrDefaultAsync(new SubDepartmentByCodeSpec(code), ct) is null)
                .WithMessage((_, code) => t["SubDepartment with Code{0} already exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await entityRepo.FirstOrDefaultAsync(new SubDepartmentByNameSpec(name), ct) is null)
                .WithMessage((_, name) => t["SubDepartment with Name {0} already exists.", name]);

        RuleFor(e => e.DepartmentId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await fatherRepo.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => t["Department {0} Not Found.", id]);
    }
}

public class UpdateSubDepartmentRequestValidator : CustomValidator<UpdateSubDepartmentRequest>
{
    public UpdateSubDepartmentRequestValidator(IReadRepository<SubDepartment> entityRepo, IReadRepository<Department> fatherRepo, IStringLocalizer<UpdateSubDepartmentRequestValidator> t)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, code, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new SubDepartmentByCodeSpec(code), ct)
                        is not SubDepartment existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, code) => t["SubDepartment {0} already Exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, name, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new SubDepartmentByNameSpec(name), ct)
                        is not SubDepartment existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, name) => t["SubDepartment {0} already Exists.", name]);

        RuleFor(e => e.DepartmentId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await fatherRepo.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => t["Department {0} Not Found.", id]);
    }
}