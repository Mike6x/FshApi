using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Property;

namespace FSH.WebApi.Application.Property.AssetStatuses;

public class ImportAssetStatusesRequest : IRequest<int>
{
    public FileUploadRequest ExcelFile { get; set; } = default!;
}

public class ImportAssetStatusesRequestHandler : IRequestHandler<ImportAssetStatusesRequest, int>
{
    private readonly IDapperRepository _repository;
    private readonly IExcelReader _excelReader;
    private readonly IStringLocalizer _localizer;
    public ImportAssetStatusesRequestHandler(
        IDapperRepository repository,
        IExcelReader excelReader,
        IStringLocalizer<ImportAssetStatusesRequestHandler> localizer)
    {
        _repository = repository;
        _excelReader = excelReader;
        _localizer = localizer;
    }

    public async Task<int> Handle(ImportAssetStatusesRequest request, CancellationToken cancellationToken)
    {
        var items = await _excelReader.ToListAsync<AssetStatus>(request.ExcelFile, FileType.Excel);
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
