using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.People;

namespace FSH.WebApi.Application.Organization.SubDepartments;

public class ImportSubDepartmentsRequest : IRequest<int>
{
    public FileUploadRequest ExcelFile { get; set; } = default!;
}

public class ImportSubDepartmentsRequestHandler : IRequestHandler<ImportSubDepartmentsRequest, int>
{
    private readonly IDapperRepository _repository;
    private readonly IExcelReader _excelReader;
    private readonly IStringLocalizer _localizer;
    public ImportSubDepartmentsRequestHandler(
        IDapperRepository repository,
        IExcelReader excelReader,
        IStringLocalizer<ImportSubDepartmentsRequestHandler> localizer)
    {
        _repository = repository;
        _excelReader = excelReader;
        _localizer = localizer;
    }

    public async Task<int> Handle(ImportSubDepartmentsRequest request, CancellationToken cancellationToken)
    {
        var items = await _excelReader.ToListAsync<SubDepartment>(request.ExcelFile, FileType.Excel);

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
