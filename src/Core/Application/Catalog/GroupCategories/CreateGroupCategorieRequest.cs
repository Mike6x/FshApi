namespace FSH.WebApi.Application.Catalog.GroupCategories;

public class CreateGroupCategorieRequest : IRequest<DefaultIdType>
{
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public CatalogType? Type { get; set; }
    public bool IsActive { get; set; }

    public DefaultIdType BusinessLineId { get; set; }
}

public class CreateGroupCategorieRequestHandler : IRequestHandler<CreateGroupCategorieRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<GroupCategorie> _repository;

    public CreateGroupCategorieRequestHandler(IRepositoryWithEvents<GroupCategorie> repository) => _repository = repository;

    public async Task<DefaultIdType> Handle(CreateGroupCategorieRequest request, CancellationToken cancellationToken)
    {
        var entity = new GroupCategorie(
                request.Order,
                request.Code,
                request.Name,
                request.Description,
                request.BusinessLineId,
                request.Type,
                request.IsActive);

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}
