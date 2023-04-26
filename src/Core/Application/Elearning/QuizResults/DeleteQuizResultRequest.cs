using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Elearning;

namespace FSH.WebApi.Application.Elearning.QuizResults;

public class DeleteQuizResultRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public DeleteQuizResultRequest(Guid id) => Id = id;
}

public class DeleteQuizResultRequestHandler : IRequestHandler<DeleteQuizResultRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<QuizResult> _repository;
    private readonly IStringLocalizer _t;

    public DeleteQuizResultRequestHandler(IRepositoryWithEvents<QuizResult> repository, IStringLocalizer<DeleteQuizResultRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(DeleteQuizResultRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity ?? throw new NotFoundException(_t["QuizResult {0} Not Found."]);

        // Add Domain Events to be raised after the commit
        entity.DomainEvents.Add(EntityDeletedEvent.WithEntity(entity));

        await _repository.DeleteAsync(entity, cancellationToken);

        return request.Id;
    }
}