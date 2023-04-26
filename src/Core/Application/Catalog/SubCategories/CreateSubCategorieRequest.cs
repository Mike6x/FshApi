namespace FSH.WebApi.Application.Catalog.SubCategories;

public class CreateSubCategorieRequest : IRequest<DefaultIdType>
{
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public CatalogType? Type { get; set; }
    public bool? IsActive { get; set; }
    public DefaultIdType CategorieId { get; set; }
}

public class CreateSubCategorieRequestHandler : IRequestHandler<CreateSubCategorieRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<SubCategorie> _repository;

    public CreateSubCategorieRequestHandler(IRepositoryWithEvents<SubCategorie> repository) => _repository = repository;

    public async Task<DefaultIdType> Handle(CreateSubCategorieRequest request, CancellationToken cancellationToken)
    {
        var entity = new SubCategorie(
                request.Order,
                request.Code,
                request.Name,
                request.Description,
                request.CategorieId,
                request.Type,
                request.IsActive);

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}
