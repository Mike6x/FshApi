using FSH.WebApi.Application.Elearning.QuizResults;
using FSH.WebApi.Domain.Elearning;

namespace FSH.WebApi.Application.Elearning.Quizs;

public class DeleteQuizRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteQuizRequest(Guid id) => Id = id;
}

public class DeleteQuizRequestHandler : IRequestHandler<DeleteQuizRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Quiz> _repository;
    private readonly IReadRepository<QuizResult> _quizResultRepo;

    private readonly IStringLocalizer _t;
    private readonly IFileStorageService _fileStorage;
    public DeleteQuizRequestHandler(
        IRepositoryWithEvents<Quiz> repository,
        IReadRepository<QuizResult> quizResultRepo,
        IStringLocalizer<DeleteQuizRequestHandler> localizer,
        IFileStorageService fileStorage) =>
        (_repository, _quizResultRepo, _t, _fileStorage) = (repository, quizResultRepo, localizer, fileStorage);

    public async Task<Guid> Handle(DeleteQuizRequest request, CancellationToken cancellationToken)
    {
        if (await _quizResultRepo.AnyAsync(new QuizResultsByQuizSpec(request.Id), cancellationToken))
        {
            throw new ConflictException(_t["Quiz cannot be deleted as it's being used."]);
        }

        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity ?? throw new NotFoundException(_t["Entity {0} Not Found."]);

        if (!string.IsNullOrEmpty(entity.QuizPath))
        {
            string root = Directory.GetCurrentDirectory();
            _fileStorage.Remove(Path.Combine(root, entity.QuizPath));
        }

        await _repository.DeleteAsync(entity, cancellationToken);

        return request.Id;
    }
}

