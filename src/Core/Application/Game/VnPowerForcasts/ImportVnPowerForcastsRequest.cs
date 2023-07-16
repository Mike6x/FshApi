using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Game;

namespace FSH.WebApi.Application.Game.VnPowerForcasts;

public class ImportVnPowerForcastsRequest : IRequest<int>
{
    public FileUploadRequest ExcelFile { get; set; } = default!;
}

public class ImportVnPowerForcastsRequestHandler : IRequestHandler<ImportVnPowerForcastsRequest, int>
{
    private readonly IDapperRepository _repository;
    private readonly IExcelReader _excelReader;
    private readonly IStringLocalizer _localizer;
    public ImportVnPowerForcastsRequestHandler(
        IDapperRepository repository,
        IExcelReader excelReader,
        IStringLocalizer<ImportVnPowerForcastsRequestHandler> localizer)
    {
        _repository = repository;
        _excelReader = excelReader;
        _localizer = localizer;
    }

    public async Task<int> Handle(ImportVnPowerForcastsRequest request, CancellationToken cancellationToken)
    {
        var items = await _excelReader.ToListAsync<VnPowerForcast>(request.ExcelFile, FileType.Excel);
        if (items?.Count > 0)
        {
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
