namespace FSH.WebApi.Application.Catalog.Categories;

public class CreateCategorieRequest : IRequest<Guid>
{
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public CatalogType? Type { get; set; }
    public bool? IsActive { get; set; }

    public Guid GroupCategorieId { get; set; }
}

public class CreateCategoryRequestHandler : IRequestHandler<CreateCategorieRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Categorie> _repository;

    public CreateCategoryRequestHandler(IRepositoryWithEvents<Categorie> repository) => _repository = repository;

    public async Task<Guid> Handle(CreateCategorieRequest request, CancellationToken cancellationToken)
    {
        var category = new Categorie(
            request.Order,
            request.Code,
            request.Name,
            request.Description,
            request.GroupCategorieId,
            request.Type,
            request.IsActive);

        await _repository.AddAsync(category, cancellationToken);

        return category.Id;
    }
}
