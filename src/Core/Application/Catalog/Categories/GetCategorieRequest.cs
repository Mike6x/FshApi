namespace FSH.WebApi.Application.Catalog.Categories;

public class GetCategorieRequest : IRequest<CategorieDetailsDto>
{
    public DefaultIdType Id { get; set; }

    public GetCategorieRequest(DefaultIdType id) => Id = id;
}

public class GetCategoryRequestHandler : IRequestHandler<GetCategorieRequest, CategorieDetailsDto>
{
    private readonly IRepository<Categorie> _repository;
    private readonly IStringLocalizer _t;

    public GetCategoryRequestHandler(IRepository<Categorie> repository, IStringLocalizer<GetCategoryRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<CategorieDetailsDto> Handle(GetCategorieRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<Categorie, CategorieDetailsDto>)new CategorieByIdIdWithGroupCategorieSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Category {0} Not Found.", request.Id]);
}
