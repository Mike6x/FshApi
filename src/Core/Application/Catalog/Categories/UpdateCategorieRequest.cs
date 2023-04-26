namespace FSH.WebApi.Application.Catalog.Categories;

public class UpdateCategorieRequest : IRequest<Guid>
{
    public int Order { get; set; }
    public DefaultIdType Id { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public CatalogType Type { get; set; }
    public bool? IsActive { get; set; }

    public DefaultIdType GroupCategorieId { get; set; }
}

public class UpdateCategoryRequestHandler : IRequestHandler<UpdateCategorieRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Categorie> _repository;
    private readonly IStringLocalizer _t;

    public UpdateCategoryRequestHandler(IRepositoryWithEvents<Categorie> repository, IStringLocalizer<UpdateCategoryRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateCategorieRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);

        entity.Update(
            request.Order,
            request.Code,
            request.Name,
            request.Description,
            request.GroupCategorieId,
            request.Type,
            request.IsActive);

        await _repository.UpdateAsync(entity, cancellationToken);

        return request.Id;
    }
}
