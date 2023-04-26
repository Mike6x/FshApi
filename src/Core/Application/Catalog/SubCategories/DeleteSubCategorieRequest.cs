using FSH.WebApi.Application.Catalog.Categories;

namespace FSH.WebApi.Application.Catalog.SubCategories;

public class DeleteSubCategorieRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }

    public DeleteSubCategorieRequest(DefaultIdType id) => Id = id;
}

public class DeleteSubCategorieRequestHandler : IRequestHandler<DeleteSubCategorieRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<SubCategorie> _repository;
    private readonly IReadRepository<Categorie> _childRepository;
    private readonly IStringLocalizer _t;

    public DeleteSubCategorieRequestHandler(IRepositoryWithEvents<SubCategorie> repository, IReadRepository<Categorie> childRepository, IStringLocalizer<DeleteSubCategorieRequestHandler> localizer) =>
        (_repository, _childRepository, _t) = (repository, childRepository, localizer);

    public async Task<DefaultIdType> Handle(DeleteSubCategorieRequest request, CancellationToken cancellationToken)
    {
        // if (await _childRepository.AnyAsync(new CategoriesBySubCategorieSpec(request.Id), cancellationToken))
        // {
        //    throw new ConflictException(_t["SubCategorie cannot be deleted as it's being used."]);
        // }

        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity ?? throw new NotFoundException(_t["SubCategorie {0} Not Found."]);

        await _repository.DeleteAsync(entity, cancellationToken);

        return request.Id;
    }
}
