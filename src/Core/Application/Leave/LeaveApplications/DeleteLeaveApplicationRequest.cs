using FSH.WebApi.Domain.Leave;

namespace FSH.WebApi.Application.Leave.LeaveApplications;

public class DeleteLeaveApplicationRequest(Guid id) : IRequest<Guid>
{
    public Guid Id { get; set; } = id;
}

public class DeleteRequestHandler : IRequestHandler<DeleteLeaveApplicationRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<LeaveApplication> _repository;
    private readonly IStringLocalizer _t;
    public DeleteRequestHandler(
        IRepositoryWithEvents<LeaveApplication> repository,
        IStringLocalizer<DeleteRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(DeleteLeaveApplicationRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity ?? throw new NotFoundException(_t["Entity {0} Not Found."]);

        if (entity.Status != RequestStatus.Edited)
        {
            throw new CustomException(_t["Entity {0} have submited already."]);
        }

        await _repository.DeleteAsync(entity, cancellationToken);

        return request.Id;
    }
}