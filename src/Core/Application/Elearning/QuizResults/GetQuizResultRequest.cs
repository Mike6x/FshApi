using FSH.WebApi.Domain.Elearning;

namespace FSH.WebApi.Application.Elearning.QuizResults;

public class GetQuizResultRequest : IRequest<QuizResultDetailsDto>
{
    public Guid Id { get; set; }
    public GetQuizResultRequest(Guid id) => Id = id;
}

public class GetQuizResultRequestHandler : IRequestHandler<GetQuizResultRequest, QuizResultDetailsDto>
{
    private readonly IRepository<QuizResult> _repository;
    private readonly IStringLocalizer _t;

    public GetQuizResultRequestHandler(IRepository<QuizResult> repository, IStringLocalizer<GetQuizResultRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<QuizResultDetailsDto> Handle(GetQuizResultRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<QuizResult, QuizResultDetailsDto>)new QuizResultByIdWithQuizSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);
}
