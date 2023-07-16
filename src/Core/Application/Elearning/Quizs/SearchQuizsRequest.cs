using FSH.WebApi.Domain.Elearning;

namespace FSH.WebApi.Application.Elearning.Quizs;

public class SearchQuizsRequest : PaginationFilter, IRequest<PaginationResponse<QuizDto>>
{
    public DefaultIdType? QuizTypeId { get; set; }
    public DefaultIdType? QuizTopicId { get; set; }
    public DefaultIdType? QuizModeId { get; set; }

    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }

    public bool? IsActive { get; set; }
}

public class SearchQuizsRequestHandler(IReadRepository<Quiz> repository) : IRequestHandler<SearchQuizsRequest, PaginationResponse<QuizDto>>
{
    private readonly IReadRepository<Quiz> _repository = repository;

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
                .Where(e => e.IsActive.Equals(request.IsActive!), request.IsActive.HasValue)
                .Where(e => e.QuizTypeId.Equals(request.QuizTypeId!), request.QuizTypeId.HasValue)
                .Where(e => e.QuizTopicId.Equals(request.QuizTopicId!), request.QuizTopicId.HasValue)
                .Where(e => e.QuizModeId.Equals(request.QuizModeId!), request.QuizModeId.HasValue)
                .Where(e => e.StartTime >= request.FromDate, request.FromDate.HasValue)
                .Where(e => e.EndTime <= request.ToDate, request.ToDate.HasValue)
                    .OrderByDescending(e => e.CreatedOn, !request.HasOrderBy());
}