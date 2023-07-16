using FSH.WebApi.Domain.Elearning;

namespace FSH.WebApi.Application.Elearning.Quizs;

public class SearchQuizsRequest : PaginationFilter, IRequest<PaginationResponse<QuizDto>>
{
    public QuizType? QuizType { get; set; }
    public QuizTopic? QuizTopic { get; set; }
    public QuizMode? QuizMode { get; set; }
    public bool? IsActive { get; set; }
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
                .OrderByDescending(e => e.CreatedOn, !request.HasOrderBy())
                    .Where(e => e.QuizType.Equals(request.QuizType!), request.QuizType.HasValue)
                    .Where(e => e.QuizTopic.Equals(request.QuizTopic!), request.QuizTopic.HasValue)
                    .Where(e => e.IsActive.Equals(request.IsActive!), request.IsActive.HasValue)
                    .Where(e => e.QuizMode.Equals(request.QuizMode!), request.QuizMode.HasValue);
}