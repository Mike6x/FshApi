namespace FSH.WebApi.Application.Catalog.BusinessLines;

public class GetBusinessLineRequest : IRequest<BusinessLineDetailsDto>
{
    public Guid Id { get; set; }
    public GetBusinessLineRequest(Guid id) => Id = id;
}

public class GetBusinessLineRequestHandler : IRequestHandler<GetBusinessLineRequest, BusinessLineDetailsDto>
{
    private readonly IRepository<BusinessLine> _repository;
    private readonly IStringLocalizer _t;

    public GetBusinessLineRequestHandler(IRepository<BusinessLine> repository, IStringLocalizer<GetBusinessLineRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<BusinessLineDetailsDto> Handle(GetBusinessLineRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<BusinessLine, BusinessLineDetailsDto>)new BusinessLineByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);
}