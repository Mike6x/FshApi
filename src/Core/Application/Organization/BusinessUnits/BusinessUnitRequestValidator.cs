using FSH.WebApi.Domain.Organization;

namespace FSH.WebApi.Application.Organization.BusinessUnits;

public class CreateBusinessUnitRequestValidator : CustomValidator<CreateBusinessUnitRequest>
{
    public CreateBusinessUnitRequestValidator(IReadRepository<BusinessUnit> entityRepo, IStringLocalizer<CreateBusinessUnitRequestValidator> T)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (code, ct) => await entityRepo.FirstOrDefaultAsync(new BusinessUnitByCodeSpec(code), ct) is null)
                .WithMessage((_, code) => T["BusinessUnit with Code{0} already exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await entityRepo.FirstOrDefaultAsync(new BusinessUnitByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["BusinessUnit with Name {0} already exists.", name]);
    }
}

public class UpdateBusinessUnitRequestValidator : CustomValidator<UpdateBusinessUnitRequest>
{
    public UpdateBusinessUnitRequestValidator(IReadRepository<BusinessUnit> entityRepo, IStringLocalizer<UpdateBusinessUnitRequestValidator> T)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, code, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new BusinessUnitByCodeSpec(code), ct)
                        is not BusinessUnit existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, code) => T["BusinessUnit {0} already Exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, name, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new BusinessUnitByNameSpec(name), ct)
                        is not BusinessUnit existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, name) => T["BusinessUnit {0} already Exists.", name]);
    }
}