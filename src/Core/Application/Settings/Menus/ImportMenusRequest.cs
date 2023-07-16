using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.Menus;

public class ImportMenusRequest : IRequest<int>
{
    public FileUploadRequest ExcelFile { get; set; } = default!;
}

public class ImportMenusRequestHandler(
    IDapperRepository repository,
    IExcelReader excelReader,
    IStringLocalizer<ImportMenusRequestHandler> localizer) : IRequestHandler<ImportMenusRequest, int>
{
    private readonly IDapperRepository _repository = repository;
    private readonly IExcelReader _excelReader = excelReader;
    private readonly IStringLocalizer _localizer = localizer;

    public async Task<int> Handle(ImportMenusRequest request, CancellationToken cancellationToken)
    {
        var items = await _excelReader.ToListAsync<Menu>(request.ExcelFile, FileType.Excel);
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