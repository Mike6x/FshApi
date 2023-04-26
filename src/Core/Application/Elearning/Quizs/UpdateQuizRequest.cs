using FSH.WebApi.Domain.Elearning;

namespace FSH.WebApi.Application.Elearning.Quizs;

public class UpdateQuizRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;

    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public bool IsActive { get; set; }
    public QuizType QuizType { get; set; }
    public QuizTopic QuizTopic { get; set; }

    public bool DeleteCurrentQuiz { get; set; }

    public FileUploadRequest? QuizMedia { get; set; }
}

public class UpdateQuizRequestHandler : IRequestHandler<UpdateQuizRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Quiz> _repository;
    private readonly IStringLocalizer _t;
    private readonly IFileStorageService _file;

    public UpdateQuizRequestHandler(IRepositoryWithEvents<Quiz> repository, IStringLocalizer<UpdateQuizRequestHandler> localizer, IFileStorageService file) =>
        (_repository, _t, _file) = (repository, localizer, file);

    public async Task<Guid> Handle(UpdateQuizRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);

        // Remove old quiz if flag is set
        if (request.DeleteCurrentQuiz)
        {
            string? currentQuizPath = entity.QuizPath;
            if (!string.IsNullOrEmpty(currentQuizPath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentQuizPath));
            }

            entity = entity.ClearQuizPath();
        }

        string? quizPath = request.QuizMedia is not null
                            ? await _file.UploadAsync<Quiz>(request.QuizMedia, FileType.QuizMedia, cancellationToken)
                            : null;

        entity.Update(
            request.Code,
            request.Name,
            request.Description,
            quizPath,
            request.StartTime,
            request.EndTime,
            request.IsActive,
            request.QuizType,
            request.QuizTopic);

        await _repository.UpdateAsync(entity, cancellationToken);

        return request.Id;
    }
}
