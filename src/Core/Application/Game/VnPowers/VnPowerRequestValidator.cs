using FSH.WebApi.Domain.Game;

namespace FSH.WebApi.Application.Game.VnPowers;

public class CreateVnPowerRequestValidator : CustomValidator<CreateVnPowerRequest>
{
    public CreateVnPowerRequestValidator(IReadRepository<VnPower> entityRepo, IStringLocalizer<CreateVnPowerRequestValidator> t)
    {
        RuleFor(e => e.DrawDate)
          .GreaterThanOrEqualTo(DateTime.Parse("2017-01-01"))
            .WithMessage(errorMessage: "VnPower start from 2017");

        RuleFor(e => e.DrawId)
            .GreaterThanOrEqualTo(0)
            .MustAsync(async (code, ct) => await entityRepo.FirstOrDefaultAsync(new VnPowerByCodeSpec(code), ct) is null)
                .WithMessage((_, code) => t["VnPower with Code{0} already exists.", code]);

        RuleFor(e => e.WinNumber1)
            .InclusiveBetween(0, 55);

        RuleFor(e => e.WinNumber2)
            .InclusiveBetween(0, 55);

        RuleFor(e => e.WinNumber3)
            .InclusiveBetween(0, 55);

        RuleFor(e => e.WinNumber4)
            .InclusiveBetween(0, 55);

        RuleFor(e => e.WinNumber5)
            .InclusiveBetween(0, 55);

        RuleFor(e => e.WinNumber6)
            .InclusiveBetween(0, 55);

        RuleFor(e => e.BonusNumber)
            .InclusiveBetween(0, 55);
    }
}

public class UpdateVnPowerRequestValidator : CustomValidator<UpdateVnPowerRequest>
{
    public UpdateVnPowerRequestValidator(IReadRepository<VnPower> entityRepo, IStringLocalizer<UpdateVnPowerRequestValidator> t)
    {
        RuleFor(e => e.DrawId)
            .GreaterThanOrEqualTo(0)
            .MustAsync(async (entity, drawId, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new VnPowerByCodeSpec(drawId), ct)
                        is not VnPower existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, drawId) => t["VnPower {0} already Exists.", drawId]);

        RuleFor(e => e.DrawDate)
          .GreaterThanOrEqualTo(DateTime.Parse("2017-01-01"));

        RuleFor(e => e.WinNumber1)
            .InclusiveBetween(0, 55);

        RuleFor(e => e.WinNumber2)
            .InclusiveBetween(0, 55);

        RuleFor(e => e.WinNumber3)
            .InclusiveBetween(0, 55);

        RuleFor(e => e.WinNumber4)
            .InclusiveBetween(0, 55);

        RuleFor(e => e.WinNumber5)
            .InclusiveBetween(0, 55);

        RuleFor(e => e.WinNumber6)
            .InclusiveBetween(0, 55);

        RuleFor(e => e.BonusNumber)
            .InclusiveBetween(0, 55);
    }
}