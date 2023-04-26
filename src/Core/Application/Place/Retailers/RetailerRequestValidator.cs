using FSH.WebApi.Domain.Place;

namespace FSH.WebApi.Application.Place.Retailers;

public class CreateRetailerRequestValidator : CustomValidator<CreateRetailerRequest>
{
    public CreateRetailerRequestValidator(IReadRepository<Retailer> entityRepo, IReadRepository<Channel> fatherRepo, IStringLocalizer<CreateRetailerRequestValidator> t)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (code, ct) => await entityRepo.FirstOrDefaultAsync(new RetailerByCodeSpec(code), ct) is null)
                .WithMessage((_, code) => t["Retailer with Code{0} already exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await entityRepo.FirstOrDefaultAsync(new RetailerByNameSpec(name), ct) is null)
                .WithMessage((_, name) => t["Retailer with Name {0} already exists.", name]);

        RuleFor(e => e.ChannelId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await fatherRepo.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => t["Channel {0} Not Found.", id]);

    }
}

public class UpdateRetailerRequestValidator : CustomValidator<UpdateRetailerRequest>
{
    public UpdateRetailerRequestValidator(IReadRepository<Retailer> entityRepo, IReadRepository<Channel> fatherRepo, IStringLocalizer<UpdateRetailerRequestValidator> t)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, code, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new RetailerByCodeSpec(code), ct)
                        is not Retailer existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, code) => t["Retailer {0} already Exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, name, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new RetailerByNameSpec(name), ct)
                        is not Retailer existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, name) => t["Retailer {0} already Exists.", name]);

        RuleFor(e => e.ChannelId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await fatherRepo.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => t["Channel {0} Not Found.", id]);
    }
}