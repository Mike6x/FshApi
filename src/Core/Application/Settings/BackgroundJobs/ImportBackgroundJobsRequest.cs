using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.BackgroundJobs;

public class ImportBackgroundJobsRequest : IRequest<int>
{
    public FileUploadRequest ExcelFile { get; set; } = default!;
}

public class ImportBackgroundJobsRequestHandler(
    IDapperRepository repository,
    IExcelReader excelReader,
    IStringLocalizer<ImportBackgroundJobsRequestHandler> localizer) : IRequestHandler<ImportBackgroundJobsRequest, int>
{
    private readonly IDapperRepository _repository = repository;
    private readonly IExcelReader _excelReader = excelReader;
    private readonly IStringLocalizer _localizer = localizer;

    public async Task<int> Handle(ImportBackgroundJobsRequest request, CancellationToken cancellationToken)
    {
        var items = await _excelReader.ToListAsync<BackgroundJob>(request.ExcelFile, FileType.Excel);
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
