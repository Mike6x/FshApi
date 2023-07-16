namespace FSH.WebApi.Application.DummyJobs;
public interface IDummyEmailService : IScopedService
{
    void SendEmail(string backGroundJobType, string startTime);
}