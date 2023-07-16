using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Communication;

namespace FSH.WebApi.Application.Communication.Chat;
public class ImportChatMessagesRequest : IRequest<int>
{
    public FileUploadRequest ExcelFile { get; set; } = default!;
}

public class ImportChatMessagesRequestHandler : IRequestHandler<ImportChatMessagesRequest, int>
{
    private readonly IDapperRepository _repository;
    private readonly IExcelReader _excelReader;
    private readonly IStringLocalizer _localizer;
    public ImportChatMessagesRequestHandler(
        IDapperRepository repository,
        IExcelReader excelReader,
        IStringLocalizer<ImportChatMessagesRequestHandler> localizer)
    {
        _repository = repository;
        _excelReader = excelReader;
        _localizer = localizer;
    }

    public async Task<int> Handle(ImportChatMessagesRequest request, CancellationToken cancellationToken)
    {
        var items = await _excelReader.ToListAsync<ChatMessage>(request.ExcelFile, FileType.Excel);

        if (items == null || items.Count == 0) throw new CustomException(_localizer["Excel file error or empty!"]);

        try
        {
            await _repository.UpdateRangeAsync(items, cancellationToken);
        }
        catch (Exception)
        {
            throw new InternalServerException(_localizer["Internal error!"]);
        }

        return items.Count;
    }
}
