using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.CronJobs;

public class CronJobByIdSpec : Specification<CronJob, CronJobDetailsDto>, ISingleResultSpecification<CronJob>
{
    public CronJobByIdSpec(DefaultIdType id) =>
        Query
            .Where(e => e.Id == id);
}

public class CronJobByCodeSpec : Specification<CronJob>, ISingleResultSpecification<CronJob>
{
    public CronJobByCodeSpec(string itemCode) =>
        Query
            .Where(e => e.Code == itemCode);
}

public class CronJobByNameSpec : Specification<CronJob>, ISingleResultSpecification<CronJob>
{
    public CronJobByNameSpec(string itemName) =>
        Query
            .Where(e => e.Name == itemName);
}