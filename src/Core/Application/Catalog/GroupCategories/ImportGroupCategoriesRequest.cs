using FSH.WebApi.Application.Common.DataIO;

namespace FSH.WebApi.Application.Catalog.GroupCategories;
public class ImportGroupCategoriesRequest : IRequest<int>
{
    public FileUploadRequest ExcelFile { get; set; } = default!;
}

public class ImportGroupCategoriesRequestHandler : IRequestHandler<ImportGroupCategoriesRequest, int>
{
    private readonly IDapperRepository _repository;
    private readonly IExcelReader _excelReader;
    private readonly IStringLocalizer _localizer;
    public ImportGroupCategoriesRequestHandler(
        IDapperRepository repository,
        IExcelReader excelReader,
        IStringLocalizer<ImportGroupCategoriesRequestHandler> localizer)
    {
        _repository = repository;
        _excelReader = excelReader;
        _localizer = localizer;
    }

    public async Task<int> Handle(ImportGroupCategoriesRequest request, CancellationToken cancellationToken)
    {
        var items = await _excelReader.ToListAsync<GroupCategorie>(request.ExcelFile, FileType.Excel);
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