using FSH.WebApi.Application.Common.DataIO;

namespace FSH.WebApi.Application.Catalog.Categories;

public class ImportCategoriesRequest : IRequest<int>
{
    public FileUploadRequest ExcelFile { get; set; } = default!;
}

public class ImportCategoriesRequestHandler : IRequestHandler<ImportCategoriesRequest, int>
{
    private readonly IDapperRepository _repository;
    private readonly IExcelReader _excelReader;
    private readonly IStringLocalizer _localizer;
    public ImportCategoriesRequestHandler(
        IDapperRepository repository,
        IExcelReader excelReader,
        IStringLocalizer<ImportCategoriesRequestHandler> localizer)
    {
        _repository = repository;
        _excelReader = excelReader;
        _localizer = localizer;
    }

    public async Task<int> Handle(ImportCategoriesRequest request, CancellationToken cancellationToken)
    {
        var items = await _excelReader.ToListAsync<Categorie>(request.ExcelFile, FileType.Excel);
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
