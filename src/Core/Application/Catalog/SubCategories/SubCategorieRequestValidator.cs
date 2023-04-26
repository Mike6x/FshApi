namespace FSH.WebApi.Application.Catalog.SubCategories;

public class CreateSubCategorieRequestValidator : CustomValidator<CreateSubCategorieRequest>
{
    public CreateSubCategorieRequestValidator(IReadRepository<SubCategorie> entityRepo, IReadRepository<Categorie> fatherRepo, IStringLocalizer<CreateSubCategorieRequestValidator> t)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (code, ct) => await entityRepo.FirstOrDefaultAsync(new SubCategorieByCodeSpec(code), ct) is null)
                .WithMessage((_, code) => t["SubCategorie with Code{0} already exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await entityRepo.FirstOrDefaultAsync(new SubCategorieByNameSpec(name), ct) is null)
                .WithMessage((_, name) => t["SubCategorie with Name {0} already exists.", name]);

        RuleFor(e => e.CategorieId)
            .NotEqual(DefaultIdType.Empty).WithMessage(t["Categorie required"])
            .NotEmpty()
            .MustAsync(async (id, ct) => await fatherRepo.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => t["Categorie {0} Not Found.", id]);

    }
}

public class UpdateSubCategorieRequestValidator : CustomValidator<UpdateSubCategorieRequest>
{
    public UpdateSubCategorieRequestValidator(IReadRepository<SubCategorie> entityRepo, IReadRepository<Categorie> fatherRepo, IStringLocalizer<UpdateSubCategorieRequestValidator> t)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, code, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new SubCategorieByCodeSpec(code), ct)
                        is not SubCategorie existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, code) => t["SubCategorie {0} already Exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, name, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new SubCategorieByNameSpec(name), ct)
                        is not SubCategorie existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, name) => t["SubCategorie {0} already Exists.", name]);

        RuleFor(e => e.CategorieId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await fatherRepo.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => t["Categorie {0} Not Found.", id]);
    }
}