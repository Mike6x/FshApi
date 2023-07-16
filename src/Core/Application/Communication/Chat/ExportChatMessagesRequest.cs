using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Communication;

namespace FSH.WebApi.Application.Communication.Chat;
public class ExportChatMessagesRequest : BaseFilter, IRequest<Stream>
{
}

public class ExportChatMessagesRequestHandler : IRequestHandler<ExportChatMessagesRequest, Stream>
{
    private readonly IRepository<ChatMessage> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportChatMessagesRequestHandler(IRepository<ChatMessage> repository, IExcelWriter excelWriter)
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