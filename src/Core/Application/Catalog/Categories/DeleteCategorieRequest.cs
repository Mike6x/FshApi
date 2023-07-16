using FSH.WebApi.Application.Catalog.SubCategories;
using FSH.WebApi.Application.Production.Products;
using FSH.WebApi.Domain.Production;

namespace FSH.WebApi.Application.Catalog.Categories;

public class DeleteCategorieRequest(Guid id) : IRequest<Guid>
{
    public DefaultIdType Id { get; set; } = id;
}

public class DeleteCategoryRequestHandler : IRequestHandler<DeleteCategorieRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Categorie> _categoryRepo;
    private readonly IReadRepository<SubCategorie> _childRepository;

    private readonly IReadRepository<Product> _productRepo;

    private readonly IStringLocalizer _t;

    public DeleteCategoryRequestHandler(IRepositoryWithEvents<Categorie> categoryRepo, IReadRepository<SubCategorie> childRepository, IReadRepository<Product> productRepo, IStringLocalizer<DeleteCategoryRequestHandler> localizer) =>
        (_categoryRepo, _childRepository, _productRepo, _t) = (categoryRepo, childRepository, productRepo, localizer);

    public async Task<Guid> Handle(DeleteCategorieRequest request, CancellationToken cancellationToken)
    {
        if (await _childRepository.AnyAsync(new SubCategoriesByCategorieSpec(request.Id), cancellationToken))
        {
            throw new ConflictException(_t["Categorie cannot be deleted as it's being used in SubCategorie table."]);
        }

        if (await _productRepo.AnyAsync(new ProductsByCategorySpec(request.Id), cancellationToken))
        {
            throw new ConflictException(_t["Categorie cannot be deleted as it's being used in Product table."]);
        }

        var category = await _categoryRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = category ?? throw new NotFoundException(_t["Category {0} Not Found."]);

        await _categoryRepo.DeleteAsync(category, cancellationToken);

        return request.Id;
    }
}