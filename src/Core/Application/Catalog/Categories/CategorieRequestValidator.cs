namespace FSH.WebApi.Application.Catalog.Categories;

public class CreateCategorieRequestValidator : CustomValidator<CreateCategorieRequest>
{
    public CreateCategorieRequestValidator(IReadRepository<Categorie> repository, IReadRepository<GroupCategorie> fatherRepo, IStringLocalizer<CreateCategorieRequestValidator> T)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (code, ct) => await repository.FirstOrDefaultAsync(new CategorieByCodeSpec(code), ct) is null)
            .WithMessage((_, code) => T["Category with code: {0} already Exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.FirstOrDefaultAsync(new CategorieByNameSpec(name), ct) is null)
            .WithMessage((_, name) => T["Category with name : {0} already Exists.", name]);

        RuleFor(e => e.GroupCategorieId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await fatherRepo.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => T["GroupCategorie {0} Not Found.", id]);
    }
}

public class UpdateCategorieRequestValidator : CustomValidator<UpdateCategorieRequest>
{
    public UpdateCategorieRequestValidator(IRepository<Categorie> repository, IReadRepository<GroupCategorie> fatherRepo, IStringLocalizer<UpdateCategorieRequestValidator> T)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (category, code, ct) =>
                await repository.FirstOrDefaultAsync(new CategorieByCodeSpec(code), ct)
                    is not Categorie existingCategory || existingCategory.Id == category.Id)
                .WithMessage((_, code) => T["Category with code: {0} already Exists.", code]);

        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (category, name, ct) =>
                await repository.FirstOrDefaultAsync(new CategorieByNameSpec(name), ct)
                        is not Categorie existingCategory || existingCategory.Id == category.Id)
                .WithMessage((_, name) => T["Category {0} already Exists.", name]);

        RuleFor(e => e.GroupCategorieId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await fatherRepo.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => T["GroupCategorie {0} Not Found.", id]);
    }
}