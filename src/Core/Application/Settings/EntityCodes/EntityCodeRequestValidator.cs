using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.EntityCodes;

public class CreateEntityCodeRequestValidator : CustomValidator<CreateEntityCodeRequest>
{
    public CreateEntityCodeRequestValidator(IReadRepository<EntityCode> entityRepo, IStringLocalizer<CreateEntityCodeRequestValidator> T)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (code, ct) => await entityRepo.FirstOrDefaultAsync(new EntityCodeByCodeSpec(code), ct) is null)
                .WithMessage((_, code) => T["EntityCode with Code{0} already exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await entityRepo.FirstOrDefaultAsync(new EntityCodeByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["EntityCode with Name {0} already exists.", name]);
    }
}

public class UpdateEntityCodeRequestValidator : CustomValidator<UpdateEntityCodeRequest>
{
    public UpdateEntityCodeRequestValidator(IReadRepository<EntityCode> entityRepo, IStringLocalizer<UpdateEntityCodeRequestValidator> T)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, code, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new EntityCodeByCodeSpec(code), ct)
                        is not EntityCode existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, code) => T["EntityCode {0} already Exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, name, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new EntityCodeByNameSpec(name), ct)
                        is not EntityCode existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, name) => T["EntityCode {0} already Exists.", name]);
    }
}