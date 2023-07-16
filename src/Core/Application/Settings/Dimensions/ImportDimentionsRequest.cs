using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.Dimensions;

public class ImportDimensionsRequest : IRequest<int>
{
    public FileUploadRequest ExcelFile { get; set; } = default!;
}

public class ImportDimensionsRequestHandler : IRequestHandler<ImportDimensionsRequest, int>
{
    private readonly IRepositoryWithEvents<Dimension> _repository;
    private readonly IExcelReader _excelReader;
    private readonly IStringLocalizer _localizer;
    public ImportDimensionsRequestHandler(
        IRepositoryWithEvents<Dimension> repository,
        IExcelReader excelReader,
        IStringLocalizer<ImportDimensionsRequestHandler> localizer)
    {
        _repository = repository;
        _excelReader = excelReader;
        _localizer = localizer;
    }

    public async Task<int> Handle(ImportDimensionsRequest request, CancellationToken cancellationToken)
    {
        var items = await _excelReader.ToListAsync<Dimension>(request.ExcelFile, FileType.Excel);
        bool errorsOccurred = false;
        var errors = new List<string>();
        int count = 0;

        if (items?.Count > 0)
        {
            foreach (var item in items)
            {
                if (item.Id == Guid.Empty)
                {
                    count++;
                    var response = await _repository.AddAsync(item, cancellationToken);
                    if (response != null)
                    {
                        count++;
                    }
                    else
                    {
                        errorsOccurred = true;
                        errors.Add(_localizer[string.Format("Error happened when importing Item named {0}", item.Name)]);
                    }
                }
                else
                {
                    var response = await _repository.GetByIdAsync(item.Id, cancellationToken);
                    if (response != null)
                    {
                        response.Update(
                            item.Order,
                            item.Code,
                            item.Name,
                            item.Description,
                            item.IsActive,
                            item.FullName,
                            item.NativeName,
                            item.FullNativeName,
                            item.Value,
                            item.Type,
                            item.FatherId);
                        await _repository.UpdateAsync(response, cancellationToken);
                        count++;
                    }
                    else
                    {
                        errorsOccurred = true;
                        errors.Add(_localizer[string.Format("Item named {0} Not Found", item.Name)]);
                    }
                }
            }

            if (errorsOccurred)
            {
                throw new InternalServerException(_localizer["Internal error:"], errors);
            }
        }

        // else
        // {
        //    throw new InvalidOperationException(_localizer["An Error has occurred when uploading!"]);
        // }
        return count;
    }
}
