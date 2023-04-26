using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Organization;

namespace FSH.WebApi.Application.Catalog.SubCategories;
public class ImportSubCategoriesRequest : IRequest<int>
{
    public FileUploadRequest ExcelFile { get; set; } = default!;
}

public class ImportSubCategoriesRequestHandler : IRequestHandler<ImportSubCategoriesRequest, int>
{
    private readonly IDapperRepository _repository;
    private readonly IExcelReader _excelReader;
    private readonly IStringLocalizer _localizer;
    public ImportSubCategoriesRequestHandler(
        IDapperRepository repository,
        IExcelReader excelReader,
        IStringLocalizer<ImportSubCategoriesRequestHandler> localizer)
    {
        _repository = repository;
        _excelReader = excelReader;
        _localizer = localizer;
    }

    public async Task<int> Handle(ImportSubCategoriesRequest request, CancellationToken cancellationToken)
    {
        var items = await _excelReader.ToListAsync<SubCategorie>(request.ExcelFile, FileType.Excel);
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
