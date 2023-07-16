using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.Dimensions;

public class CreateDimensionRequestValidator : CustomValidator<CreateDimensionRequest>
{
    public CreateDimensionRequestValidator(IReadRepository<Dimension> entityRepo, IStringLocalizer<CreateDimensionRequestValidator> T)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (code, ct) => await entityRepo.FirstOrDefaultAsync(new DimensionByCodeSpec(code), ct) is null)
                .WithMessage((_, code) => T["Entity with Code{0} already exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await entityRepo.FirstOrDefaultAsync(new DimensionByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["Entity with Name {0} already exists.", name]);
    }
}

public class UpdateDimensionRequestValidator : CustomValidator<UpdateDimensionRequest>
{
    public UpdateDimensionRequestValidator(IReadRepository<Dimension> entityRepo, IStringLocalizer<UpdateDimensionRequestValidator> T)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, code, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new DimensionByCodeSpec(code), ct)
                        is not Dimension existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, code) => T["Entity {0} already Exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, name, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new DimensionByNameSpec(name), ct)
                        is not Dimension existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, name) => T["Entity {0} already Exists.", name]);
    }
}