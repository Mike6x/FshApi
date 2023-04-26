namespace FSH.WebApi.Application.Catalog.GroupCategories;

public class GetGroupCategorieRequest : IRequest<GroupCategorieDetailsDto>
{
    public DefaultIdType Id { get; set; }
    public GetGroupCategorieRequest(DefaultIdType id) => Id = id;
}

public class GetGroupCategorieRequestHandler : IRequestHandler<GetGroupCategorieRequest, GroupCategorieDetailsDto>
{
    private readonly IRepository<GroupCategorie> _repository;
    private readonly IStringLocalizer _t;

    public GetGroupCategorieRequestHandler(IRepository<GroupCategorie> repository, IStringLocalizer<GetGroupCategorieRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<GroupCategorieDetailsDto> Handle(GetGroupCategorieRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<GroupCategorie, GroupCategorieDetailsDto>)new GroupCategorieByIdWithBusinessLineSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);
}