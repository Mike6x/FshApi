using FSH.WebApi.Application.Catalog.GroupCategories;

namespace FSH.WebApi.Application.Catalog.BusinessLines;

public class DeleteBusinessLineRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteBusinessLineRequest(Guid id) => Id = id;
}

public class DeleteBusinessLineRequestHandler : IRequestHandler<DeleteBusinessLineRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<BusinessLine> _repository;
    private readonly IReadRepository<GroupCategorie> _childRepository;
    private readonly IStringLocalizer _t;

    public DeleteBusinessLineRequestHandler(IRepositoryWithEvents<BusinessLine> repository, IReadRepository<GroupCategorie> childRepository, IStringLocalizer<DeleteBusinessLineRequestHandler> localizer) =>
        (_repository, _childRepository, _t) = (repository, childRepository, localizer);

    public async Task<Guid> Handle(DeleteBusinessLineRequest request, CancellationToken cancellationToken)
    {
        if (await _childRepository.AnyAsync(new GroupCategoriesByBusinessLineSpec(request.Id), cancellationToken))
        {
            throw new ConflictException(_t["BusinessLine cannot be deleted as it's being used."]);
        }

        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity ?? throw new NotFoundException(_t["BusinessLine {0} Not Found."]);

        await _repository.DeleteAsync(entity, cancellationToken);

        return request.Id;
    }
}
