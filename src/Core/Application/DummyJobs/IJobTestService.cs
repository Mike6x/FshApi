namespace FSH.WebApi.Application.DummyJobs;
public interface IJobTestService : IScopedService
{
    void FireAndForgetJob();
    void ReccuringJob();
    void DelayedJob();
    void ContinuationJob();
}