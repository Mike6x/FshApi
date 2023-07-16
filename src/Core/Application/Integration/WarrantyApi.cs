using FSH.WebApi.Application.Common.Netsuite;
using FSH.WebApi.Domain.Integration;
using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Integration;

public class WarrantyApi(INetsuiteService netsuiteService, IDapperRepository repository, IRepository<CronJob> cronJobRepository) : IWarrantyApi
{
    private readonly INetsuiteService _netsuiteService = netsuiteService;
    private readonly IRepository<CronJob> _cronJobRepository = cronJobRepository;
    private readonly IDapperRepository _repository = repository;
    public async Task<int> GetAndUpdateSerialsAsync(Dictionary<string, string>? parameters)
    {
        parameters ??= new Dictionary<string, string>
                        {
                            { "type", "serial" },
                            { "modify_to", DateTime.Today.AddDays(1).ToString("yyyy-MM-dd") },
                            { "modify_from", DateTime.Today.ToString("yyyy-MM-dd")},
                            { "serial_num", string.Empty }
                        };

        var data = await _netsuiteService.GetAsync<ApiSerialResult>(parameters) ?? throw new InternalServerException(string.Format("An error occurred while connect to external source"));

        var apiCronJob = new CronJob
        {
            Code = nameof(CommandList.GetSerialsFromErp) + "_" + DateTime.UtcNow.ToString("yyyyMMdd_HHmm"),
            Name = nameof(CommandList.GetSerialsFromErp) + "_" + DateTime.UtcNow.ToString("yyyyMMdd_HHmm"),

            FromDate = DateTime.Parse(parameters["modify_to"]),
            ToDate = DateTime.Parse(parameters["modify_from"]),
            TotalRecord = data.TotalRecord
        };

        await _cronJobRepository.AddAsync(apiCronJob);

        if (data?.Records.Count > 0)
        {
            foreach (var record in data.Records)
            {
                record.CronJobId = apiCronJob.Id;
                switch (MockImport())
                {
                    case 1:
                        record.ImportStatus = "Success";
                        apiCronJob.NumberOfSuccessed++;
                        break;
                    case 2:
                        record.ImportStatus = "Existed";
                        apiCronJob.NumberOfExisted++;
                        break;
                    case 3:
                        record.ImportStatus = "Duplicated";
                        apiCronJob.NumberOfDuplicated++;
                        break;
                    case 4:
                        record.ImportStatus = "OthersFailed";
                        apiCronJob.NumberOfDuplicated++;
                        break;
                }
            }

            await _repository.UpdateRangeAsync(data.Records);
            await _cronJobRepository.UpdateAsync(apiCronJob);
        }

        return data?.TotalRecord ?? 0;
    }

    public async Task<List<ApiSerial>> GetSerialsAsync(Dictionary<string, string>? parameters)
    {
        parameters ??= new Dictionary<string, string>
                        {
                            { "type", "serial" },
                            { "modify_to", DateTime.Today.AddDays(1).ToString("yyyy-MM-dd") },
                            { "modify_from", DateTime.Today.ToString("yyyy-MM-dd")},
                            { "serial_num", string.Empty }
                        };

        var data = await _netsuiteService.GetAsync<ApiSerialResult>(parameters) ?? throw new InternalServerException(string.Format("An error occurred while connect to external source"));

        var apiCronJob = new CronJob
        {
            Code = nameof(CommandList.GetSerialsFromErp) + "_" + DateTime.UtcNow.ToString("yyyyMMdd_HHmm"),
            Name = nameof(CommandList.GetSerialsFromErp) + "_" + DateTime.UtcNow.ToString("yyyyMMdd_HHmm"),

            FromDate = DateTime.Parse(parameters["modify_to"]),
            ToDate = DateTime.Parse(parameters["modify_from"]),
            TotalRecord = data.TotalRecord
        };

        await _cronJobRepository.AddAsync(apiCronJob);

        if (data?.Records.Count > 0)
        {
            foreach (var record in data.Records)
            {
                record.CronJobId = apiCronJob.Id;
                switch (MockImport())
                {
                    case 1:
                        record.ImportStatus = "Success";
                        apiCronJob.NumberOfSuccessed++;
                        break;
                    case 2:
                        record.ImportStatus = "Existed";
                        apiCronJob.NumberOfExisted++;
                        break;
                    case 3:
                        record.ImportStatus = "Duplicated";
                        apiCronJob.NumberOfDuplicated++;
                        break;
                    case 4:
                        record.ImportStatus = "OthersFailed";
                        apiCronJob.NumberOfDuplicated++;
                        break;
                }
            }

            await _repository.UpdateRangeAsync(data.Records);
            await _cronJobRepository.UpdateAsync(apiCronJob);
        }

        return data != null ? data.Records : [];
    }

    private static int MockImport()
    {
        return new Random().Next(1, 4);
    }
}
