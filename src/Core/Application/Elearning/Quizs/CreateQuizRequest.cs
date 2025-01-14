﻿using FSH.WebApi.Domain.Elearning;

namespace FSH.WebApi.Application.Elearning.Quizs;

public class CreateQuizRequest : IRequest<Guid>
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public bool IsActive { get; set; }

    public DefaultIdType QuizTypeId { get; set; }
    public DefaultIdType QuizTopicId { get; set; }
    public DefaultIdType QuizModeId { get; set; }

    public decimal Price { get; private set; }

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
        string mediaPath = await _file.UploadAsync<Quiz>(request.QuizMedia, FileType.QuizMedia, cancellationToken);

        string? quizPath = string.Empty;
        if (!string.IsNullOrEmpty(mediaPath))
        {
            quizPath = _file.UnZip(mediaPath);
            _file.RemoveFile(mediaPath);
        }

        var entity = new Quiz(
                request.Code,
                request.Name,
                request.Description ?? string.Empty,
                quizPath ?? string.Empty,
                request.StartTime,
                request.EndTime,
                request.IsActive,
                request.QuizTypeId,
                request.QuizTopicId,
                request.QuizModeId,
                request.Price,
                null,
                null,
                null);

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}
