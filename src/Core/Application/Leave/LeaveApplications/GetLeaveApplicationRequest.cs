using FSH.WebApi.Domain.Leave;

namespace FSH.WebApi.Application.Leave.LeaveApplications;

public class GetLeaveApplicationRequest(Guid id) : IRequest<LeaveApplicationDetailsDto>
{
    public Guid Id { get; set; } = id;
}

public class GetRequestHandler : IRequestHandler<GetLeaveApplicationRequest, LeaveApplicationDetailsDto>
{
    private readonly IRepository<LeaveApplication> _repository;
    private readonly IStringLocalizer _t;

    public GetRequestHandler(IRepository<LeaveApplication> repository, IStringLocalizer<GetRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<LeaveApplicationDetailsDto> Handle(GetLeaveApplicationRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<LeaveApplication, LeaveApplicationDetailsDto>)new LeaveApplicationByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);
}
