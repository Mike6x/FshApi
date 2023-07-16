// using FSH.WebApi.Domain.Settings;
// namespace FSH.WebApi.Application.Settings.BackgroundJobs;
// public class CreateBackgroundJobRequestValidator : CustomValidator<CreateBackgroundJobRequest>
// {
//    public CreateBackgroundJobRequestValidator(IReadRepository<BackgroundJob> entityRepo, IStringLocalizer<CreateBackgroundJobRequestValidator> T)
//    {
//        RuleFor(e => e.Code)
//            .NotEmpty()
//            .MaximumLength(75)
//            .MustAsync(async (code, ct) => await entityRepo.FirstOrDefaultAsync(new BackgroundJobByCodeSpec(code), ct) is null)
//                .WithMessage((_, code) => T["BackgroundJob with Code{0} already exists.", code]);
//        RuleFor(e => e.Name)
//            .NotEmpty()
//            .MaximumLength(75);
//            // .MustAsync(async (name, ct) => await entityRepo.FirstOrDefaultAsync(new BackgroundJobByNameSpec(name), ct) is null)
//            //    .WithMessage((_, name) => T["BackgroundJob with Name {0} already exists.", name]);
//    }
// }

// public class UpdateBackgroundJobRequestValidator : CustomValidator<UpdateBackgroundJobRequest>
// {
//    public UpdateBackgroundJobRequestValidator(IReadRepository<BackgroundJob> entityRepo, IStringLocalizer<UpdateBackgroundJobRequestValidator> T)
//    {
//         RuleFor(e => e.Code)
//            .NotEmpty()
//            .MaximumLength(75)
//            .MustAsync(async (entity, code, ct) =>
//                    await entityRepo.FirstOrDefaultAsync(new BackgroundJobByCodeSpec(code), ct)
//                        is not BackgroundJob existingEntity || existingEntity.Id == entity.Id)
//                .WithMessage((_, code) => T["BackgroundJob {0} already Exists.", code]);
//        RuleFor(e => e.Name)
//            .NotEmpty()
//            .MaximumLength(75);
//            // .MustAsync(async (entity, name, ct) =>
//            //        await entityRepo.FirstOrDefaultAsync(new BackgroundJobByNameSpec(name), ct)
//            //            is not BackgroundJob existingEntity || existingEntity.Id == entity.Id)
//            //    .WithMessage((_, name) => T["BackgroundJob {0} already Exists.", name]);
//    }
// }