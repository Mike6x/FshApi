using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Elearning;

namespace FSH.WebApi.Application.Elearning.Quizs;

public class ExportQuizsRequest : BaseFilter, IRequest<Stream>
{
    public QuizType? QuizType { get; set; }
    public QuizTopic? QuizTopic { get; set; }
    public bool? IsActive { get; set; }
    public QuizMode? QuizMode { get; set; }
}

public class ExportQuizsRequestHandler : IRequestHandler<ExportQuizsRequest, Stream>
{
    private readonly IReadRepository<Quiz> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportQuizsRequestHandler(IReadRepository<Quiz> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

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
            .Where(e => e.QuizType.Equals(request.QuizType!.HasValue), request.QuizType.HasValue)
            .Where(e => e.QuizTopic.Equals(request.QuizType!.HasValue), request.QuizTopic.HasValue)
            .Where(e => e.IsActive.Equals(request.IsActive!), request.IsActive.HasValue)
            .Where(e => e.QuizMode.Equals(request.QuizMode!.HasValue), request.QuizMode.HasValue);
}