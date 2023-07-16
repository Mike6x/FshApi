using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.CronJobs;

public class CreateCronJobRequestValidator : CustomValidator<CreateCronJobRequest>
{
    public CreateCronJobRequestValidator(IReadRepository<CronJob> entityRepo, IStringLocalizer<CreateCronJobRequestValidator> T)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (code, ct) => await entityRepo.FirstOrDefaultAsync(new CronJobByCodeSpec(code), ct) is null)
                .WithMessage((_, code) => T["CronJob with Code{0} already exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await entityRepo.FirstOrDefaultAsync(new CronJobByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["CronJob with Name {0} already exists.", name]);
    }
}

public class UpdateCronJobRequestValidator : CustomValidator<UpdateCronJobRequest>
{
    public UpdateCronJobRequestValidator(IReadRepository<CronJob> entityRepo, IStringLocalizer<UpdateCronJobRequestValidator> T)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, code, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new CronJobByCodeSpec(code), ct)
                        is not CronJob existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, code) => T["CronJob {0} already Exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, name, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new CronJobByNameSpec(name), ct)
                        is not CronJob existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, name) => T["CronJob {0} already Exists.", name]);
    }
}