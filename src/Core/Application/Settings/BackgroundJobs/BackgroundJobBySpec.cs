using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.BackgroundJobs;

public class BackgroundJobByIdSpec : Specification<BackgroundJob, BackgroundJobDetailsDto>, ISingleResultSpecification<BackgroundJob>
{
    public BackgroundJobByIdSpec(DefaultIdType id) =>
        Query
            .Where(e => e.Id == id);
}

public class BackgroundJobByCodeSpec : Specification<BackgroundJob>, ISingleResultSpecification<BackgroundJob>
{
    public BackgroundJobByCodeSpec(string code) =>
        Query
            .Where(e => e.Code == code);
}

public class BackgroundJobByNameSpec : Specification<BackgroundJob>, ISingleResultSpecification<BackgroundJob>
{
    public BackgroundJobByNameSpec(string name) =>
        Query
            .Where(e => e.Name == name);
}