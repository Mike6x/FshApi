using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Elearning;

namespace FSH.WebApi.Application.Elearning.Quizs;

public class ExportQuizsRequest : BaseFilter, IRequest<Stream>
{
    public DefaultIdType? QuizTypeId { get; set; }
    public DefaultIdType? QuizTopicId { get; set; }
    public DefaultIdType? QuizModeId { get; set; }

    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }

    public bool? IsActive { get; set; }
}

public class ExportQuizsRequestHandler(IReadRepository<Quiz> repository, IExcelWriter excelWriter) : IRequestHandler<ExportQuizsRequest, Stream>
{
    private readonly IReadRepository<Quiz> _repository = repository;
    private readonly IExcelWriter _excelWriter = excelWriter;

    public async Task<Stream> Handle(ExportQuizsRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportQuizsSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportQuizsSpecification : EntitiesByBaseFilterSpec<Quiz, QuizExportDto>
{
    public ExportQuizsSpecification(ExportQuizsRequest request)
        : base(request) =>
        Query
            .Where(e => e.IsActive.Equals(request.IsActive!), request.IsActive.HasValue)
            .Where(e => e.QuizTypeId.Equals(request.QuizTypeId!), request.QuizTypeId.HasValue)
            .Where(e => e.QuizTopicId.Equals(request.QuizTopicId!), request.QuizTopicId.HasValue)
            .Where(e => e.QuizModeId.Equals(request.QuizModeId!), request.QuizModeId.HasValue)
            .Where(e => e.StartTime >= request.FromDate, request.FromDate.HasValue)
            .Where(e => e.EndTime <= request.ToDate, request.ToDate.HasValue)
                .OrderByDescending(e => e.CreatedOn);
}