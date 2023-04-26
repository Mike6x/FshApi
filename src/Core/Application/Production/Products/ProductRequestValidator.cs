using FSH.WebApi.Domain.Production;

namespace FSH.WebApi.Application.Production.Products;

public class CreateProductRequestValidator : CustomValidator<CreateProductRequest>
{
    public CreateProductRequestValidator(IReadRepository<Product> productRepo, IReadRepository<Brand> brandRepo, IStringLocalizer<CreateProductRequestValidator> T)
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await productRepo.FirstOrDefaultAsync(new ProductByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["Product {0} already Exists.", name]);

        RuleFor(p => p.ListPrice)
            .GreaterThanOrEqualTo(0);

        RuleFor(p => p.Image);

        RuleFor(p => p.BrandId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await brandRepo.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => T["Brand {0} Not Found.", id]);
    }
}

public class UpdateProductRequestValidator : CustomValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator(IReadRepository<Product> productRepo, IReadRepository<Brand> brandRepo, IStringLocalizer<UpdateProductRequestValidator> T)
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (product, name, ct) =>
                    await productRepo.FirstOrDefaultAsync(new ProductByNameSpec(name), ct)
                        is not Product existingProduct || existingProduct.Id == product.Id)
                .WithMessage((_, name) => T["Product {0} already Exists.", name]);

        RuleFor(p => p.ListPrice)
            .GreaterThanOrEqualTo(0);

        RuleFor(p => p.Image);

        RuleFor(p => p.BrandId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await brandRepo.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => T["Brand {0} Not Found.", id]);
    }
}