using FSH.WebApi.Domain.Organization;

namespace FSH.WebApi.Application.Catalog.SubCategories;

public class GetSubCategorieRequest : IRequest<SubCategorieDetailsDto>
{
    public DefaultIdType Id { get; set; }
    public GetSubCategorieRequest(DefaultIdType id) => Id = id;
}

public class GetSubCategorieRequestHandler : IRequestHandler<GetSubCategorieRequest, SubCategorieDetailsDto>
{
    private readonly IRepository<SubCategorie> _repository;
    private readonly IStringLocalizer _t;

    public GetSubCategorieRequestHandler(IRepository<SubCategorie> repository, IStringLocalizer<GetSubCategorieRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<SubCategorieDetailsDto> Handle(GetSubCategorieRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<SubCategorie, SubCategorieDetailsDto>)new SubCategorieByIdWithCategorieSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);
}