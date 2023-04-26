namespace FSH.WebApi.Application.Catalog.BusinessLines;

public class CreateBusinessLineRequestValidator : CustomValidator<CreateBusinessLineRequest>
{
    public CreateBusinessLineRequestValidator(IReadRepository<BusinessLine> entityRepo, IStringLocalizer<CreateBusinessLineRequestValidator> T)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (code, ct) => await entityRepo.FirstOrDefaultAsync(new BusinessLineByCodeSpec(code), ct) is null)
                .WithMessage((_, code) => T["BusinessLine with Code{0} already exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await entityRepo.FirstOrDefaultAsync(new BusinessLineByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["BusinessLine with Name {0} already exists.", name]);
    }
}

public class UpdateBusinessLineRequestValidator : CustomValidator<UpdateBusinessLineRequest>
{
    public UpdateBusinessLineRequestValidator(IReadRepository<BusinessLine> entityRepo, IStringLocalizer<UpdateBusinessLineRequestValidator> T)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, code, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new BusinessLineByCodeSpec(code), ct)
                        is not BusinessLine existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, code) => T["BusinessLine {0} already Exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, name, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new BusinessLineByNameSpec(name), ct)
                        is not BusinessLine existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, name) => T["BusinessLine {0} already Exists.", name]);
    }
}