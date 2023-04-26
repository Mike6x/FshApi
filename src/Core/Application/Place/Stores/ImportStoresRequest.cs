using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Place;

namespace FSH.WebApi.Application.Place.Stores;

public class ImportStoresRequest : IRequest<int>
{
    public FileUploadRequest ExcelFile { get; set; } = default!;
}

public class ImportStoresRequestHandler : IRequestHandler<ImportStoresRequest, int>
{
    private readonly IDapperRepository _repository;
    private readonly IExcelReader _excelReader;
    private readonly IStringLocalizer _localizer;
    public ImportStoresRequestHandler(
        IDapperRepository repository,
        IExcelReader excelReader,
        IStringLocalizer<ImportStoresRequestHandler> localizer)
    {
        _repository = repository;
        _excelReader = excelReader;
        _localizer = localizer;
    }

    public async Task<int> Handle(ImportStoresRequest request, CancellationToken cancellationToken)
    {
        var items = await _excelReader.ToListAsync<Store>(request.ExcelFile, FileType.Excel);

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
