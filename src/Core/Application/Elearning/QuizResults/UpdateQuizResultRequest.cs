using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Elearning;

namespace FSH.WebApi.Application.Elearning.QuizResults;

public class UpdateQuizResultRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public Guid QuizId { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }

    // Student Point, Used time, Time spent on taking the quiz
    public string? SId { get; set; }
    public decimal? Sp { get; set; }
    public decimal? Ut { get; set; }
    public string? Fut { get; set; }

    // Quiz Title, Passing score, Passing score in percent, Total score, Time limit
    public string? Qt { get; set; }
    public decimal? Tp { get; set; }
    public decimal? Ps { get; set; }
    public decimal? Psp { get; set; }
    public decimal? Tl { get; set; }

    // Quiz version, type : Graded
    public string? V { get; set; }
    public string? T { get; set; }
    public decimal? Rating { get; set; }
}

public class UpdateQuizResultRequestHandler : IRequestHandler<UpdateQuizResultRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<QuizResult> _repository;
    private readonly IStringLocalizer _t;

    public UpdateQuizResultRequestHandler(IRepositoryWithEvents<QuizResult> repository, IStringLocalizer<UpdateQuizResultRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateQuizResultRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity
        ?? throw new NotFoundException(_t["QuizResult {0} Not Found.", request.Id]);

        var updatedEntity = entity.Update(
                request.QuizId,
                request.StartTime,
                request.EndTime,
                request.SId,
                request.Sp,
                request.Ut,
                request.Fut,
                request.Qt,
                request.Tp,
                request.Ps,
                request.Psp,
                request.Tl,
                request.V,
                request.T,
                request.Rating);

        // Add Domain Events to be raised after the commit
        entity.DomainEvents.Add(EntityUpdatedEvent.WithEntity(entity));

        await _repository.UpdateAsync(updatedEntity, cancellationToken);

        return request.Id;
    }
}