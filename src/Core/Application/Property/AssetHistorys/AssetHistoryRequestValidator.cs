using FSH.WebApi.Domain.Property;

namespace FSH.WebApi.Application.Property.AssetHistorys;

public class CreateAssetHistoryRequestValidator : CustomValidator<CreateAssetHistoryRequest>
{
    public CreateAssetHistoryRequestValidator(IReadRepository<AssetHistory> entityRepo, IStringLocalizer<CreateAssetHistoryRequestValidator> t)
    {

        RuleFor(e => e.AssetId)
            .NotEmpty()
                .WithMessage(" The Asset is required");

        RuleFor(e => e.QualityStatusId)
            .NotEmpty()
                .WithMessage(" The QualityStatus is required");

        RuleFor(e => e.UsingStatusId)
            .NotEmpty()
                .WithMessage(" The UsingStatus is required");
    }
}

public class UpdateAssetHistoryRequestValidator : CustomValidator<UpdateAssetHistoryRequest>
{
    public UpdateAssetHistoryRequestValidator(IReadRepository<AssetHistory> entityRepo, IStringLocalizer<UpdateAssetHistoryRequestValidator> t)
    {
        RuleFor(e => e.AssetId)
            .NotEmpty()
                .WithMessage(" The Asset is required");

        RuleFor(e => e.QualityStatusId)
            .NotEmpty()
                .WithMessage(" The QualityStatus is required");

        RuleFor(e => e.UsingStatusId)
            .NotEmpty()
                .WithMessage(" The UsingStatus is required");
    }
}