using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Integration;

namespace FSH.WebApi.Application.Integration.ApiSerials;

public class ImportApiSerialsRequest : IRequest<int>
{
    public FileUploadRequest ExcelFile { get; set; } = default!;
}

public class ImportApiSerialsRequestHandler(
    IDapperRepository repository,
    IExcelReader excelReader,
    IStringLocalizer<ImportApiSerialsRequestHandler> localizer) : IRequestHandler<ImportApiSerialsRequest, int>
{
    private readonly IDapperRepository _repository = repository;
    private readonly IExcelReader _excelReader = excelReader;
    private readonly IStringLocalizer _localizer = localizer;

    public async Task<int> Handle(ImportApiSerialsRequest request, CancellationToken cancellationToken)
    {
        var items = await _excelReader.ToListAsync<ApiSerial>(request.ExcelFile, FileType.Excel);
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
