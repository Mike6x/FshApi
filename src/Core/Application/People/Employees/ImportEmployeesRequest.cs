using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.People;

namespace FSH.WebApi.Application.People.Employees;

public class ImportEmployeesRequest : IRequest<int>
{
    public FileUploadRequest ExcelFile { get; set; } = default!;
}

public class ImportEmployeesRequestHandler : IRequestHandler<ImportEmployeesRequest, int>
{
    private readonly IDapperRepository _repository;
    private readonly IExcelReader _excelReader;
    private readonly IStringLocalizer _localizer;
    public ImportEmployeesRequestHandler(
        IDapperRepository repository,
        IExcelReader excelReader,
        IStringLocalizer<ImportEmployeesRequestHandler> localizer)
    {
        _repository = repository;
        _excelReader = excelReader;
        _localizer = localizer;
    }

    public async Task<int> Handle(ImportEmployeesRequest request, CancellationToken cancellationToken)
    {
        var items = await _excelReader.ToListAsync<Employee>(request.ExcelFile, FileType.Excel);
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
