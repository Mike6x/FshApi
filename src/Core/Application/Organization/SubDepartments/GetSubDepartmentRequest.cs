using FSH.WebApi.Domain.People;

namespace FSH.WebApi.Application.Organization.SubDepartments;

public class GetSubDepartmentRequest : IRequest<SubDepartmentDetailsDto>
{
    public Guid Id { get; set; }
    public GetSubDepartmentRequest(Guid id) => Id = id;
}

public class GetSubDepartmentRequestHandler : IRequestHandler<GetSubDepartmentRequest, SubDepartmentDetailsDto>
{
    private readonly IRepository<SubDepartment> _repository;
    private readonly IStringLocalizer _t;

    public GetSubDepartmentRequestHandler(IRepository<SubDepartment> repository, IStringLocalizer<GetSubDepartmentRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<SubDepartmentDetailsDto> Handle(GetSubDepartmentRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<SubDepartment, SubDepartmentDetailsDto>)new SubDepartmentByIdWithDepartmentSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);
}