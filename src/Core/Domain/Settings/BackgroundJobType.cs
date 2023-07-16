namespace FSH.WebApi.Domain.Settings;
public enum BackgroundJobType
{
    All,
    FireAndForget,
    Delayed,
    Continuations,
    Hourly,
    Daily,
    Weekly,
    Monthy
}