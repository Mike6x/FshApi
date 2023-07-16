using FSH.WebApi.Domain.Elearning;

namespace FSH.WebApi.Application.Elearning.QuizResults;

public class SearchQuizResultsRequest : PaginationFilter, IRequest<PaginationResponse<QuizResultDto>>
{
    public Guid? QuizId { get; set; }
}

public class SearchQuizResultsRequestHandler : IRequestHandler<SearchQuizResultsRequest, PaginationResponse<QuizResultDto>>
{
    private readonly IReadRepository<QuizResult> _repository;

    public SearchQuizResultsRequestHandler(IReadRepository<QuizResult> repository) => _repository = repository;

    public async Task<PaginationResponse<QuizResultDto>> Handle(SearchQuizResultsRequest request, CancellationToken cancellationToken)
    {
        var spec = new SearchQuizResultsRequestSpecification(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}

public class SearchQuizResultsRequestSpecification : EntitiesByPaginationFilterSpec<QuizResult, QuizResultDto>
{
    public SearchQuizResultsRequestSpecification(SearchQuizResultsRequest request)
        : base(request) =>
        Query
            .Include(e => e.Quiz)
            .Where(e => e.QuizId.Equals(request.QuizId!.Value), request.QuizId.HasValue)
            .OrderByDescending(e => e.EndTime, !request.HasOrderBy());
}
