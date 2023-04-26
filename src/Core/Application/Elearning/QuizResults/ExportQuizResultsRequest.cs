using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Elearning;

namespace FSH.WebApi.Application.Elearning.QuizResults;

public class ExportQuizResultsRequest : BaseFilter, IRequest<Stream>
{
    public Guid? QuizId { get; set; }

}

public class ExportQuizResultsRequestHandler : IRequestHandler<ExportQuizResultsRequest, Stream>
{
    private readonly IReadRepository<QuizResult> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportQuizResultsRequestHandler(IReadRepository<QuizResult> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportQuizResultsRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportQuizResultsSpec(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportQuizResultsSpec : EntitiesByBaseFilterSpec<QuizResult, QuizResultExportDto>
{
    public ExportQuizResultsSpec(ExportQuizResultsRequest request)
        : base(request) =>
        Query
            .Include(e => e.Quiz)
            .Where(e => e.QuizId.Equals(request.QuizId!.Value), request.QuizId.HasValue);
}