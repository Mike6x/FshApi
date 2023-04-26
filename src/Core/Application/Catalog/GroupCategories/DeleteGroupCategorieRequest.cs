using FSH.WebApi.Application.Catalog.Categories;

namespace FSH.WebApi.Application.Catalog.GroupCategories;

public class DeleteGroupCategorieRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }

    public DeleteGroupCategorieRequest(DefaultIdType id) => Id = id;
}

public class DeleteGroupCategorieRequestHandler : IRequestHandler<DeleteGroupCategorieRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<GroupCategorie> _repository;
    private readonly IReadRepository<Categorie> _childRepository;
    private readonly IStringLocalizer _t;

    public DeleteGroupCategorieRequestHandler(IRepositoryWithEvents<GroupCategorie> repository, IReadRepository<Categorie> childRepository, IStringLocalizer<DeleteGroupCategorieRequestHandler> localizer) =>
        (_repository, _childRepository, _t) = (repository, childRepository, localizer);

    public async Task<DefaultIdType> Handle(DeleteGroupCategorieRequest request, CancellationToken cancellationToken)
    {
        if (await _childRepository.AnyAsync(new CategoriesByGroupCategorieSpec(request.Id), cancellationToken))
        {
            throw new ConflictException(_t["GroupCategorie cannot be deleted as it's being used."]);
        }

        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity ?? throw new NotFoundException(_t["GroupCategorie {0} Not Found."]);

        await _repository.DeleteAsync(entity, cancellationToken);

        return request.Id;
    }
}
