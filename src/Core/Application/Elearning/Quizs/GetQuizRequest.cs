using FSH.WebApi.Domain.Elearning;

namespace FSH.WebApi.Application.Elearning.Quizs;

public class GetQuizRequest : IRequest<QuizDetailsDto>
{
    public Guid Id { get; set; }
    public GetQuizRequest(Guid id) => Id = id;
}

public class GetQuizRequestHandler : IRequestHandler<GetQuizRequest, QuizDetailsDto>
{
    private readonly IRepository<Quiz> _repository;
    private readonly IStringLocalizer _t;

    public GetQuizRequestHandler(IRepository<Quiz> repository, IStringLocalizer<GetQuizRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<QuizDetailsDto> Handle(GetQuizRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<Quiz, QuizDetailsDto>)new QuizByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);
}
