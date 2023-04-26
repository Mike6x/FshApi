using FSH.WebApi.Domain.Price;

namespace FSH.WebApi.Application.Price.PriceGroups;

public class CreatePriceGroupRequestValidator : CustomValidator<CreatePriceGroupRequest>
{
    public CreatePriceGroupRequestValidator(IReadRepository<PriceGroup> entityRepo, IStringLocalizer<CreatePriceGroupRequestValidator> T)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (code, ct) => await entityRepo.FirstOrDefaultAsync(new PriceGroupByCodeSpec(code), ct) is null)
                .WithMessage((_, code) => T["PriceGroup with Code{0} already exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await entityRepo.FirstOrDefaultAsync(new PriceGroupByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["PriceGroup with Name {0} already exists.", name]);
    }
}

public class UpdatePriceGroupRequestValidator : CustomValidator<UpdatePriceGroupRequest>
{
    public UpdatePriceGroupRequestValidator(IReadRepository<PriceGroup> entityRepo, IStringLocalizer<UpdatePriceGroupRequestValidator> T)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, code, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new PriceGroupByCodeSpec(code), ct)
                        is not PriceGroup existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, code) => T["PriceGroup {0} already Exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, name, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new PriceGroupByNameSpec(name), ct)
                        is not PriceGroup existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, name) => T["PriceGroup {0} already Exists.", name]);
    }
}