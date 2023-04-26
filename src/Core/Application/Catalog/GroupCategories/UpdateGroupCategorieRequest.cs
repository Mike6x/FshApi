namespace FSH.WebApi.Application.Catalog.GroupCategories;

public class UpdateGroupCategorieRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public CatalogType Type { get; set; }
    public bool? IsActive { get; set; }

    public DefaultIdType BusinessLineId { get; set; }
}

public class UpdateGroupCategorieRequestHandler : IRequestHandler<UpdateGroupCategorieRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<GroupCategorie> _repository;
    private readonly IStringLocalizer _t;

    public UpdateGroupCategorieRequestHandler(IRepositoryWithEvents<GroupCategorie> repository, IStringLocalizer<UpdateGroupCategorieRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<DefaultIdType> Handle(UpdateGroupCategorieRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);

        entity.Update(
            request.Order,
            request.Code,
            request.Name,
            request.Description,
            request.BusinessLineId,
            request.Type,
            request.IsActive);

        await _repository.UpdateAsync(entity, cancellationToken);

        return request.Id;
    }
}