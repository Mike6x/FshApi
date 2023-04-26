using FSH.WebApi.Domain.Property;

namespace FSH.WebApi.Application.Property.Assets;

public class CreateAssetRequestValidator : CustomValidator<CreateAssetRequest>
{
    public CreateAssetRequestValidator(IReadRepository<Asset> entityRepo, IStringLocalizer<CreateAssetRequestValidator> t)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (code, ct) => await entityRepo.FirstOrDefaultAsync(new AssetByCodeSpec(code), ct) is null)
                .WithMessage((_, code) => t["Asset with Code{0} already exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await entityRepo.FirstOrDefaultAsync(new AssetByNameSpec(name), ct) is null)
                .WithMessage((_, name) => t["Asset with Name {0} already exists.", name]);

        RuleFor(e => e.CategorieId)
            .NotEmpty()
                .WithMessage("The Catagorie is required");

        RuleFor(e => e.UsingStatusId)
            .NotEmpty()
                .WithMessage("The UsingStatus is required");

        RuleFor(e => e.QualityStatusId)
            .NotEmpty()
                .WithMessage("The QualityStatus is required");

        RuleFor(e => e.VendorId)
            .NotEmpty()
                .WithMessage("The Vendor is required");

    }
}

public class UpdateAssetRequestValidator : CustomValidator<UpdateAssetRequest>
{
    public UpdateAssetRequestValidator(IReadRepository<Asset> entityRepo, IStringLocalizer<UpdateAssetRequestValidator> t)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, code, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new AssetByCodeSpec(code), ct)
                        is not Asset existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, code) => t["Asset {0} already Exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, name, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new AssetByNameSpec(name), ct)
                        is not Asset existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, name) => t["Asset {0} already Exists.", name]);

        RuleFor(e => e.CategorieId)
            .NotEmpty()
                .WithMessage("The Catagorie is required");

        RuleFor(e => e.UsingStatusId)
            .NotEmpty()
                .WithMessage("The UsingStatus is required");

        RuleFor(e => e.QualityStatusId)
            .NotEmpty()
                .WithMessage("The QualityStatus is required");

        RuleFor(e => e.VendorId)
            .NotEmpty()
                .WithMessage("The Vendor is required");
    }
}