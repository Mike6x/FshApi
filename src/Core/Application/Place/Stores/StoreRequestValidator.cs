using FSH.WebApi.Domain.Place;

namespace FSH.WebApi.Application.Place.Stores;

public class CreateStoreRequestValidator : CustomValidator<CreateStoreRequest>
{
    public CreateStoreRequestValidator(IReadRepository<Store> entityRepo, IReadRepository<Retailer> fatherRepo, IStringLocalizer<CreateStoreRequestValidator> t)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (code, ct) => await entityRepo.FirstOrDefaultAsync(new StoreByCodeSpec(code), ct) is null)
                .WithMessage((_, code) => t["Store with Code{0} already exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await entityRepo.FirstOrDefaultAsync(new StoreByNameSpec(name), ct) is null)
                .WithMessage((_, name) => t["Store with Name {0} already exists.", name]);

        RuleFor(e => e.RetailerId)
            .NotNull()
            .NotEmpty().WithMessage(t["Channel required"])
            .MustAsync(async (id, ct) => await fatherRepo.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => t["Channel {0} Not Found.", id]);

    }
}

public class UpdateStoreRequestValidator : CustomValidator<UpdateStoreRequest>
{
    public UpdateStoreRequestValidator(IReadRepository<Store> entityRepo, IReadRepository<Retailer> fatherRepo, IStringLocalizer<UpdateStoreRequestValidator> t)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, code, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new StoreByCodeSpec(code), ct)
                        is not Store existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, code) => t["Store {0} already Exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, name, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new StoreByNameSpec(name), ct)
                        is not Store existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, name) => t["Store {0} already Exists.", name]);

        RuleFor(e => e.RetailerId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await fatherRepo.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => t["Channel {0} Not Found.", id]);
    }
}