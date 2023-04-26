namespace FSH.WebApi.Application.Catalog.GroupCategories;

public class CreateGroupCategorieRequestValidator : CustomValidator<CreateGroupCategorieRequest>
{
    public CreateGroupCategorieRequestValidator(IReadRepository<GroupCategorie> entityRepo, IReadRepository<BusinessLine> fatherRepo, IStringLocalizer<CreateGroupCategorieRequestValidator> t)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (code, ct) => await entityRepo.FirstOrDefaultAsync(new GroupCategorieByCodeSpec(code), ct) is null)
                .WithMessage((_, code) => t["GroupCategorie with Code{0} already exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await entityRepo.FirstOrDefaultAsync(new GroupCategorieByNameSpec(name), ct) is null)
                .WithMessage((_, name) => t["GroupCategorie with Name {0} already exists.", name]);

        RuleFor(e => e.BusinessLineId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await fatherRepo.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => t["BusinessUnit {0} Not Found.", id]);
    }
}

public class UpdateGroupCategorieRequestValidator : CustomValidator<UpdateGroupCategorieRequest>
{
    public UpdateGroupCategorieRequestValidator(IReadRepository<GroupCategorie> entityRepo, IReadRepository<BusinessLine> fatherRepo, IStringLocalizer<UpdateGroupCategorieRequestValidator> t)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, code, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new GroupCategorieByCodeSpec(code), ct)
                        is not GroupCategorie existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, code) => t["GroupCategorie {0} already Exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, name, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new GroupCategorieByNameSpec(name), ct)
                        is not GroupCategorie existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, name) => t["GroupCategorie {0} already Exists.", name]);

        RuleFor(e => e.BusinessLineId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await fatherRepo.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => t["BusinessUnit {0} Not Found.", id]);
    }
}