using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.BackgroundJobs;

public class UpdateBackgroundJobRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public BackgroundJobType Type { get; set; }
    public CommandList Command { get; set; }
    public DateTime? RunTime { get; set; }

    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public int? RepeatTimes { get; set; }
    public DefaultIdType FatherId { get; set; }
}

public class UpdateBackgroundJobRequestHandler : IRequestHandler<UpdateBackgroundJobRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<BackgroundJob> _repository;
    private readonly IStringLocalizer _t;

    public UpdateBackgroundJobRequestHandler(IRepositoryWithEvents<BackgroundJob> repository, IStringLocalizer<UpdateBackgroundJobRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<DefaultIdType> Handle(UpdateBackgroundJobRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);

        entity.Update(
            request.Code,
            request.Name,
            request.Description,
            request.Type,
            request.Command,
            request.RunTime,
            request.FromDate,
            request.ToDate,
            request.RepeatTimes,
            request.FatherId);

        await _repository.UpdateAsync(entity, cancellationToken);

        return request.Id;
    }
}