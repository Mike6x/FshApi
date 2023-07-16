using FSH.WebApi.Application.Elearning.QuizResults;
using FSH.WebApi.Domain.Elearning;

namespace FSH.WebApi.Application.Elearning.Quizs;

public class GetQuizResultsRequest : BaseFilter, IRequest<List<QuizResultDto>>
{
    public string? QuizId { get; set; }
    public string? UserId { get; set; }
}

public class GetQuizResultsRequestHandler : IRequestHandler<GetQuizResultsRequest, List<QuizResultDto>>
{
    private readonly IRepository<QuizResult> _repository;
    public GetQuizResultsRequestHandler(IRepository<QuizResult> repository)
        => _repository = repository;

    public async Task<List<QuizResultDto>> Handle(GetQuizResultsRequest request, CancellationToken cancellationToken)
    {
        var spec = new GetQuizResultsSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);
        return list ?? new List<QuizResultDto>();
    }
}

public class GetQuizResultsSpecification : EntitiesByBaseFilterSpec<QuizResult, QuizResultDto>
{
    public GetQuizResultsSpecification(GetQuizResultsRequest request)
        : base(request) =>
            Query
                .OrderBy(e => e.EndTime)
                    .Where(e => e.QuizId.ToString().Equals(request.QuizId), !string.IsNullOrEmpty(request.QuizId))
                    .Where(e => e.SId.Equals(request.UserId), !string.IsNullOrEmpty(request.UserId));
}
