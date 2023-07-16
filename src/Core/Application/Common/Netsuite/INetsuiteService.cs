namespace FSH.WebApi.Application.Common.Netsuite;
public interface INetsuiteService : ITransientService
{
    public Task<TResponse?> GetAsync<TResponse>(IEnumerable<KeyValuePair<string, string>>? args = null);

    public Task<TResponse?> PostAsync<TResponse, TRequest>(TRequest body);
}