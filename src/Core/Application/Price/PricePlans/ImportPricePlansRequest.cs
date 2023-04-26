using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Price;

namespace FSH.WebApi.Application.Price.PricePlans;

public class ImportPricePlansRequest : IRequest<int>
{
    public FileUploadRequest ExcelFile { get; set; } = default!;
}

public class ImportPricePlansRequestHandler : IRequestHandler<ImportPricePlansRequest, int>
{
    private readonly IDapperRepository _repository;
    private readonly IExcelReader _excelReader;
    private readonly IStringLocalizer _localizer;
    public ImportPricePlansRequestHandler(
        IDapperRepository repository,
        IExcelReader excelReader,
        IStringLocalizer<ImportPricePlansRequestHandler> localizer)
    {
        _repository = repository;
        _excelReader = excelReader;
        _localizer = localizer;
    }

    public async Task<int> Handle(ImportPricePlansRequest request, CancellationToken cancellationToken)
    {
        var items = await _excelReader.ToListAsync<PricePlan>(request.ExcelFile, FileType.Excel);

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
