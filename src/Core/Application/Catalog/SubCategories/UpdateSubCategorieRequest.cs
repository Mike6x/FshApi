namespace FSH.WebApi.Application.Catalog.SubCategories;

public class UpdateSubCategorieRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public CatalogType Type { get; set; }
    public bool? IsActive { get; set; }

    public Guid CategorieId { get; set; }
}

public class UpdateSubCategorieRequestHandler : IRequestHandler<UpdateSubCategorieRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<SubCategorie> _repository;
    private readonly IStringLocalizer _t;

    public UpdateSubCategorieRequestHandler(IRepositoryWithEvents<SubCategorie> repository, IStringLocalizer<UpdateSubCategorieRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateSubCategorieRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);

        entity.Update(
            request.Order,
            request.Code,
            request.Name,
            request.Description,
            request.CategorieId,
            request.Type,
            request.IsActive);

        await _repository.UpdateAsync(entity, cancellationToken);

        return request.Id;
    }
}