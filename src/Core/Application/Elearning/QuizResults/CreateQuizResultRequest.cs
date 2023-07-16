using FSH.WebApi.Domain.Elearning;

namespace FSH.WebApi.Application.Elearning.QuizResults;

public class CreateQuizResultRequest : IRequest<Guid>
{
    public Guid QuizId { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// Student Point, Used time, Time spent on taking the quiz.
    /// </summary>
    public string? SId { get; set; }
    public decimal Sp { get; set; }
    public decimal Ut { get; set; }
    public string Fut { get; set; } = default!;

    /// <summary>
    /// Quiz Title,Total point, Passing score, Passing score in percent, Time limit.
    /// </summary>
    public string Qt { get; set; } = default!;
    public decimal Tp { get; set; }
    public decimal Ps { get; set; }
    public decimal Psp { get; set; }
    public decimal Tl { get; set; }

    /// <summary>
    /// Quiz version, type : Graded.
    /// </summary>
    public string V { get; set; } = default!;
    public string T { get; set; } = default!;
    public decimal? Rating { get; set; }
}

public class CreateQuizResultRequestHandler : IRequestHandler<CreateQuizResultRequest, Guid>
{
    private readonly IRepositoryWithEvents<QuizResult> _repository;
    private readonly IReadRepository<Quiz> _quizRepo;
    private readonly IStringLocalizer _t;
    public CreateQuizResultRequestHandler(
        IRepositoryWithEvents<QuizResult> repository,
        IReadRepository<Quiz> quizRepo,
        IStringLocalizer<CreateQuizResultRequestHandler> localizer) =>
        (_repository, _quizRepo, _t) = (repository, quizRepo, localizer);

    public async Task<Guid> Handle(CreateQuizResultRequest request, CancellationToken cancellationToken)
    {
        var quiz = await _quizRepo.GetByIdAsync(request.QuizId, cancellationToken);

        _ = quiz ?? throw new NotFoundException(_t["Quiz with Id: {0} Not Found."]);

        bool isPass = request.Sp >= request.Ps;
        var entity = new QuizResult(
                            request.QuizId,
                            request.StartTime ?? DateTime.UtcNow - TimeSpan.FromSeconds((double)request.Ut),
                            request.EndTime ?? DateTime.UtcNow,
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
                            request.Rating,
                            isPass);

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}
