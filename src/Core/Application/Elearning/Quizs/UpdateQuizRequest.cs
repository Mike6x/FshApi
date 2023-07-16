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
    public QuizMode QuizMode { get; set; }
    public bool DeleteCurrentQuiz { get; set; }
    public int? Sale { get; set; }
    public decimal? Price { get; set; }
    public int? RatingCount { get; set; }
    public decimal? Rating { get; set; }

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
            if (!string.IsNullOrEmpty(entity.QuizPath))
            {
                _file.RemoveFolder(entity.QuizPath);
            }

            entity = entity.ClearQuizPath();
        }

        string? mediaPath = request.QuizMedia is not null
                            ? await _file.UploadAsync<Quiz>(request.QuizMedia, FileType.QuizMedia, cancellationToken)
                            : null;

        string? quizPath = mediaPath;
        if (!string.IsNullOrEmpty(mediaPath))
        {
            quizPath = _file.UnZip(mediaPath);
            _file.RemoveFile(mediaPath);
        }

        entity.Update(
            request.Code,
            request.Name,
            request.Description,
            quizPath,
            request.StartTime,
            request.EndTime,
            request.IsActive,
            request.QuizType,
            request.QuizTopic,
            request.QuizMode,
            request.Price,
            request.Sale,
            request.RatingCount,
            request.Rating);

        await _repository.UpdateAsync(entity, cancellationToken);

        return request.Id;
    }
}
