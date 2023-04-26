using FSH.WebApi.Domain.Elearning;

namespace FSH.WebApi.Application.Elearning.Quizs;

public class CreateQuizRequest : IRequest<Guid>
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public bool IsActive { get; set; }
    public QuizType QuizType { get; set; } = QuizType.graded;
    public QuizTopic QuizTopic { get; set; } = QuizTopic.General;

    public FileUploadRequest? QuizMedia { get; set; }
}

public class CreateQuizRequestHandler : IRequestHandler<CreateQuizRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Quiz> _repository;
    private readonly IFileStorageService _file;

    public CreateQuizRequestHandler(IRepositoryWithEvents<Quiz> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(CreateQuizRequest request, CancellationToken cancellationToken)
    {
        string quizPath = await _file.UploadAsync<Quiz>(request.QuizMedia, FileType.QuizMedia, cancellationToken);
        var entity = new Quiz(
                request.Code,
                request.Name,
                request.Description ?? string.Empty,
                quizPath,
                request.StartTime,
                request.EndTime,
                request.IsActive,
                request.QuizType,
                request.QuizTopic);

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}
