using FSH.WebApi.Domain.Elearning;

namespace FSH.WebApi.Application.Elearning.Quizs;

public class SearchQuizsRequest : PaginationFilter, IRequest<PaginationResponse<QuizDto>>
{
    public QuizType? QuizType { get; set; }
    public QuizTopic? QuizTopic { get; set; }
}

public class SearchQuizsRequestHandler : IRequestHandler<SearchQuizsRequest, PaginationResponse<QuizDto>>
{
    private readonly IReadRepository<Quiz> _repository;

    public SearchQuizsRequestHandler(IReadRepository<Quiz> repository) => _repository = repository;

    public async Task<PaginationResponse<QuizDto>> Handle(SearchQuizsRequest request, CancellationToken cancellationToken)
    {
        var spec = new SearchQuizsRequestSpecification(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}

public class SearchQuizsRequestSpecification : EntitiesByPaginationFilterSpec<Quiz, QuizDto>
{
    public SearchQuizsRequestSpecification(SearchQuizsRequest request)
        : base(request) =>
            Query
                .Where(e => e.QuizType.Equals(request.QuizType!.HasValue), request.QuizType.HasValue)
                .Where(e => e.QuizTopic.Equals(request.QuizType!.HasValue), request.QuizTopic.HasValue)
                .OrderBy(e => e.Code, !request.HasOrderBy());
}