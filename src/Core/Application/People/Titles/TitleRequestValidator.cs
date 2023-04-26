using FSH.WebApi.Domain.People;

namespace FSH.WebApi.Application.People.Titles;

public class CreateTitleRequestValidator : CustomValidator<CreateTitleRequest>
{
    public CreateTitleRequestValidator(IReadRepository<Title> entityRepo, IStringLocalizer<CreateTitleRequestValidator> T)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (code, ct) => await entityRepo.FirstOrDefaultAsync(new TitleByCodeSpec(code), ct) is null)
                .WithMessage((_, code) => T["Title with Code{0} already exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await entityRepo.FirstOrDefaultAsync(new TitleByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["Title with Name {0} already exists.", name]);
    }
}

public class UpdateTitleRequestValidator : CustomValidator<UpdateTitleRequest>
{
    public UpdateTitleRequestValidator(IReadRepository<Title> entityRepo, IStringLocalizer<UpdateTitleRequestValidator> T)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, code, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new TitleByCodeSpec(code), ct)
                        is not Title existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, code) => T["Title {0} already Exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, name, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new TitleByNameSpec(name), ct)
                        is not Title existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, name) => T["Title {0} already Exists.", name]);
    }
}