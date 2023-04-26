using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Application.Common.Models;
using FSH.WebApi.Application.Common.Persistence;
using FSH.WebApi.Application.Common.Specification;
using MediatR;

namespace FSH.WebApi.Infrastructure.Chat;

public class ExportChatMessagesRequest : BaseFilter, IRequest<Stream>
{
}

public class ExportChatMessagesRequestHandler : IRequestHandler<ExportChatMessagesRequest, Stream>
{
    private readonly IReadRepository<ChatMessage> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportChatMessagesRequestHandler(IReadRepository<ChatMessage> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportChatMessagesRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportChatMessagesSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportChatMessagesSpecification : EntitiesByBaseFilterSpec<ChatMessage, ChatMessageExportDto>
{
    public ExportChatMessagesSpecification(ExportChatMessagesRequest request)
        : base(request) =>
        Query
           .SearchBy(request);

}