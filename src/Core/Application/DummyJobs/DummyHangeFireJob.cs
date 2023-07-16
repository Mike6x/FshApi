namespace FSH.WebApi.Application.DummyJobs;

public interface IDummyHangeFireJob : IScopedService
{
    Task Execute();
}

internal class DummyHangeFireJob(ILogger<DummyHangeFireJob> logger) : IDummyHangeFireJob
{
    public Task Execute()
    {
        logger.LogWarning("A Test Hangfire Job.");

        return Task.CompletedTask;
    }
}

internal static class JobIds
{
    public const string MyRecurringJob = nameof(MyRecurringJob);
}