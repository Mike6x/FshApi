using FSH.WebApi.Application.DummyJobs;
using Microsoft.Extensions.Logging;

namespace FSH.WebApi.Infrastructure.BackgroundJobs.Jobs;

public class DummyEmailService(ILogger<DummyEmailService> logger) : IDummyEmailService
{
    public void SendEmail(string backGroundJobType, string startTime)
    {
        logger.LogWarning(backGroundJobType + " - " + startTime + "in UTC - Email Sent - " + DateTime.Now.ToLongTimeString());
    }
}