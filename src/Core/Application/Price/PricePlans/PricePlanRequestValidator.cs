using FSH.WebApi.Domain.Price;

namespace FSH.WebApi.Application.Price.PricePlans;

public class CreatePricePlanRequestValidator : CustomValidator<CreatePricePlanRequest>
{
    public CreatePricePlanRequestValidator(IReadRepository<PricePlan> entityRepo, IReadRepository<PriceGroup> fatherRepo, IStringLocalizer<CreatePricePlanRequestValidator> t)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (code, ct) => await entityRepo.FirstOrDefaultAsync(new PricePlanByCodeSpec(code), ct) is null)
                .WithMessage((_, code) => t["PricePlan with Code{0} already exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await entityRepo.FirstOrDefaultAsync(new PricePlanByNameSpec(name), ct) is null)
                .WithMessage((_, name) => t["PricePlan with Name {0} already exists.", name]);

        RuleFor(e => e.PriceGroupId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await fatherRepo.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => t["PriceGroup {0} Not Found.", id]);

    }
}

public class UpdatePricePlanRequestValidator : CustomValidator<UpdatePricePlanRequest>
{
    public UpdatePricePlanRequestValidator(IReadRepository<PricePlan> entityRepo, IReadRepository<PriceGroup> fatherRepo, IStringLocalizer<UpdatePricePlanRequestValidator> t)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, code, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new PricePlanByCodeSpec(code), ct)
                        is not PricePlan existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, code) => t["PricePlan {0} already Exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, name, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new PricePlanByNameSpec(name), ct)
                        is not PricePlan existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, name) => t["PricePlan {0} already Exists.", name]);

        RuleFor(e => e.PriceGroupId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await fatherRepo.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => t["PriceGroup {0} Not Found.", id]);
    }
}