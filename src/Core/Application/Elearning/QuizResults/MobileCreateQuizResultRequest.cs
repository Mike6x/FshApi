using FSH.WebApi.Domain.Elearning;

namespace FSH.WebApi.Application.Elearning.QuizResults;

public class MobileCreateQuizResultRequest : IRequest<Guid>
{
    // public Guid QuizId { get; set; }

    // Student Name, Email, Point, time, Time spent on taking the quiz
    // public string Sn { get; set; } = default!;
    // public string Se { get; set; } = default!;

    // Student Point, Used time, Time spent on taking the quiz
    public decimal Sp { get; set; }
    public decimal Ut { get; set; }
    public string Fut { get; set; } = default!;

    // Quiz Title, Passing score, Passing score in percent, Total score, Time limit
    public string Qt { get; set; } = default!;
    public decimal Tp { get; set; }
    public decimal Ps { get; set; }
    public decimal Psp { get; set; }
    public decimal Tl { get; set; }

    // Quiz version, type : Graded
    public string V { get; set; } = default!;
    public string T { get; set; } = default!;
}

public class MobileCreateQuizResultRequestHandler : IRequestHandler<MobileCreateQuizResultRequest, Guid>
{
    private readonly IRepositoryWithEvents<QuizResult> _repository;

    public MobileCreateQuizResultRequestHandler(IRepositoryWithEvents<QuizResult> repository) => _repository = repository;

    public async Task<Guid> Handle(MobileCreateQuizResultRequest request, CancellationToken cancellationToken)
    {
        var quizId = new Guid("c1ce5f0f-f26c-4ad2-ec8f-08da8da18c04");
        var entity = new QuizResult(
                            quizId,
                            DateTime.UtcNow,
                            DateTime.UtcNow,
                            request.Sp,
                            request.Ut,
                            request.Fut,
                            request.Qt,
                            request.Tp,
                            request.Ps,
                            request.Psp,
                            request.Tl,
                            request.V,
                            request.T);

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}
