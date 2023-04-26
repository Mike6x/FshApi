using FSH.WebApi.Domain.Property;

namespace FSH.WebApi.Application.Property.AssetStatuses;

public class CreateAssetStatusRequestValidator : CustomValidator<CreateAssetStatusRequest>
{
    public CreateAssetStatusRequestValidator(IReadRepository<AssetStatus> entityRepo, IStringLocalizer<CreateAssetStatusRequestValidator> T)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (code, ct) => await entityRepo.FirstOrDefaultAsync(new AssetStatusByCodeSpec(code), ct) is null)
                .WithMessage((_, code) => T["AssetStatus with Code{0} already exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await entityRepo.FirstOrDefaultAsync(new AssetStatusByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["AssetStatus with Name {0} already exists.", name]);
    }
}

public class UpdateAssetStatusRequestValidator : CustomValidator<UpdateAssetStatusRequest>
{
    public UpdateAssetStatusRequestValidator(IReadRepository<AssetStatus> entityRepo, IStringLocalizer<UpdateAssetStatusRequestValidator> T)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, code, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new AssetStatusByCodeSpec(code), ct)
                        is not AssetStatus existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, code) => T["AssetStatus {0} already Exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, name, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new AssetStatusByNameSpec(name), ct)
                        is not AssetStatus existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, name) => T["AssetStatus {0} already Exists.", name]);
    }
}