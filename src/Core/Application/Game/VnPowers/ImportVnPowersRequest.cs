using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Game;

namespace FSH.WebApi.Application.Game.VnPowers;

public class ImportVnPowersRequest : IRequest<int>
{
    public FileUploadRequest ExcelFile { get; set; } = default!;
}

public class ImportVnPowersRequestHandler : IRequestHandler<ImportVnPowersRequest, int>
{
    private readonly IDapperRepository _repository;
    private readonly IExcelReader _excelReader;
    private readonly IStringLocalizer _localizer;
    public ImportVnPowersRequestHandler(
        IDapperRepository repository,
        IExcelReader excelReader,
        IStringLocalizer<ImportVnPowersRequestHandler> localizer)
    {
        _repository = repository;
        _excelReader = excelReader;
        _localizer = localizer;
    }

    public async Task<int> Handle(ImportVnPowersRequest request, CancellationToken cancellationToken)
    {
        var items = await _excelReader.ToListAsync<VnPower>(request.ExcelFile, FileType.Excel);
        if (items?.Count > 0)
        {
            foreach (var item in items)
            {
                item.VnPowerResult ??= new VnPowerResult();
                item.VnPowerResult = item.VnPowerResult.ConvertFromWinNumber(
                    item.WinNumber1,
                    item.WinNumber2,
                    item.WinNumber3,
                    item.WinNumber4,
                    item.WinNumber5,
                    item.WinNumber6,
                    item.BonusNumber);

                item.VnPowerResult.Id = item.Id;

                item.VnPowerForcast ??= new VnPowerForcast();
                item.VnPowerForcast.Id = item.Id;
            }

            try
            {
                await _repository.UpdateRangeAsync(items, cancellationToken);
            }
            catch (Exception)
            {
                throw new InternalServerException(_localizer["Internal error!"]);
            }
        }

        return items?.Count ?? 0;
    }
}
