using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Organization;

namespace FSH.WebApi.Application.Organization.Departments;

public class ImportDepartmentsRequest : IRequest<int>
{
    public FileUploadRequest ExcelFile { get; set; } = default!;
}

public class ImportDepartmentsRequestHandler : IRequestHandler<ImportDepartmentsRequest, int>
{
    private readonly IDapperRepository _repository;
    private readonly IExcelReader _excelReader;
    private readonly IStringLocalizer _localizer;
    public ImportDepartmentsRequestHandler(
        IDapperRepository repository,
        IExcelReader excelReader,
        IStringLocalizer<ImportDepartmentsRequestHandler> localizer)
    {
        _repository = repository;
        _excelReader = excelReader;
        _localizer = localizer;
    }

    public async Task<int> Handle(ImportDepartmentsRequest request, CancellationToken cancellationToken)
    {
        var items = await _excelReader.ToListAsync<Department>(request.ExcelFile, FileType.Excel);

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
