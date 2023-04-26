using FSH.WebApi.Domain.Elearning;

namespace FSH.WebApi.Application.Elearning.QuizResults;

public class CreateQuizResultRequest : IRequest<Guid>
{
    public Guid QuizId { get; set; }

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

public class CreateQuizResultRequestHandler : IRequestHandler<CreateQuizResultRequest, Guid>
{
    private readonly IRepositoryWithEvents<QuizResult> _repository;

    public CreateQuizResultRequestHandler(IRepositoryWithEvents<QuizResult> repository) => _repository = repository;

    public async Task<Guid> Handle(CreateQuizResultRequest request, CancellationToken cancellationToken)
    {
        var entity = new QuizResult(
                            request.QuizId,
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
