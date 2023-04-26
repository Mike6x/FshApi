using FSH.WebApi.Domain.Organization;

namespace FSH.WebApi.Application.Organization.BusinessUnits;

public class GetBusinessUnitRequest : IRequest<BusinessUnitDetailsDto>
{
    public Guid Id { get; set; }
    public GetBusinessUnitRequest(Guid id) => Id = id;
}

public class GetBusinessUnitRequestHandler : IRequestHandler<GetBusinessUnitRequest, BusinessUnitDetailsDto>
{
    private readonly IRepository<BusinessUnit> _repository;
    private readonly IStringLocalizer _t;

    public GetBusinessUnitRequestHandler(IRepository<BusinessUnit> repository, IStringLocalizer<GetBusinessUnitRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<BusinessUnitDetailsDto> Handle(GetBusinessUnitRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<BusinessUnit, BusinessUnitDetailsDto>)new BusinessUnitByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);
}