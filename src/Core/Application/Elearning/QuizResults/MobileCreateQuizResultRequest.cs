using FSH.WebApi.Domain.Elearning;

namespace FSH.WebApi.Application.Elearning.QuizResults;

public class MobileCreateQuizResultRequest : IRequest<Guid>
{
    public Guid QuizId { get; set; }

    // Student Id, Point, time, Time spent on taking the quiz
    public string? SId { get; set; }
    public decimal Sp { get; set; }
    public decimal Ut { get; set; }
    public string Fut { get; set; } = default!;

    // Quiz Title,Total score, Passing score, Passing score in percent, Time limit
    public string Qt { get; set; } = default!;
    public decimal Tp { get; set; }
    public decimal Ps { get; set; }
    public decimal Psp { get; set; }
    public decimal Tl { get; set; }

    // Quiz version, type : Graded
    public string V { get; set; } = default!;
    public string T { get; set; } = default!;
    public decimal? Rating { get; set; }
}

public class MobileCreateQuizResultRequestHandler : IRequestHandler<MobileCreateQuizResultRequest, Guid>
{
    private readonly IRepositoryWithEvents<QuizResult> _repository;
    private readonly IReadRepository<Quiz> _quizRepo;
    private readonly IStringLocalizer _t;

    public MobileCreateQuizResultRequestHandler(
        IRepositoryWithEvents<QuizResult> repository,
        IReadRepository<Quiz> quizRepo,
        IStringLocalizer<MobileCreateQuizResultRequestHandler> localizer) =>
        (_repository, _quizRepo, _t) = (repository, quizRepo, localizer);

    public async Task<Guid> Handle(MobileCreateQuizResultRequest request, CancellationToken cancellationToken)
    {
        var quiz = await _quizRepo.GetByIdAsync(request.QuizId, cancellationToken);

        _ = quiz ?? throw new NotFoundException(_t["Quiz with Id: {0} Not Found."]);

        bool isPass = request.Sp >= request.Ps;

        var entity = new QuizResult(
                            request.QuizId,
                            DateTime.UtcNow - TimeSpan.FromSeconds((double)request.Ut),
                            DateTime.UtcNow,
                            request.SId ?? string.Empty,
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
