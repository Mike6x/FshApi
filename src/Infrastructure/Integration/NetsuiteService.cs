using FSH.WebApi.Application.Common.Netsuite;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace FSH.WebApi.Infrastructure.Integration;

public class NetsuiteService(IOptions<NetsuiteSettings> apiConfig) : INetsuiteService
{
    private readonly NetsuiteSettings _apiConfig = apiConfig.Value;

    public async Task<TResponse?> PostAsync<TResponse, TRequest>(TRequest body)
    {
        string url = "https://" + _apiConfig.AccountId + ".restlets.api.netsuite.com/app/site/hosting/restlet.nl?script=" + _apiConfig.ScriptId + "&deploy=" + _apiConfig.DeploymentId;

        var httpRequest = new HttpRequestMessage
        {
            RequestUri = new Uri(url),
            Method = HttpMethod.Post,
            Content = new StringContent(JsonConvert.SerializeObject(body))
        };

        string authHeader = OAuthBase.GenerateAuthorizationHeaderValue(new Uri(url), _apiConfig.ClientId, _apiConfig.ClientSecret, _apiConfig.TokenId, _apiConfig.TokenSecret, "POST", _apiConfig.Realm);

        httpRequest.Headers.Add("Authorization", authHeader);
        httpRequest.Headers.Add("Accept", "application/json");

        var client = new HttpClient();
        var httpResponse = await client.SendAsync(httpRequest);

        if (httpResponse.IsSuccessStatusCode)
        {
            string responseJson = await httpResponse.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResponse>(responseJson, new JsonSerializerSettings
            {
                DateFormatString = "dd/MM/yyyy h:mm tt",
            });
        }
        else
        {
            // Do something when request fails
            return default;
        }
    }

    public async Task<TResponse?> GetAsync<TResponse>(IEnumerable<KeyValuePair<string, string>>? args = null)
    {
        string url = "https://" + _apiConfig.AccountId + ".restlets.api.netsuite.com/app/site/hosting/restlet.nl?script=" + _apiConfig.ScriptId + "&deploy=" + _apiConfig.DeploymentId;

        if (args != null)
        {
            foreach (var query in args)
            {
                url += "&" + query.Key + "=" + query.Value;
            }
        }

        var httpRequest = new HttpRequestMessage
        {
            RequestUri = new Uri(url),
            Method = HttpMethod.Get,
            Content = new StringContent(string.Empty, Encoding.UTF8, "application/json")
        };

        string authHeader = OAuthBase.GenerateAuthorizationHeaderValue(new Uri(url), _apiConfig.ClientId, _apiConfig.ClientSecret, _apiConfig.TokenId, _apiConfig.TokenSecret, "GET", _apiConfig.Realm);

        httpRequest.Headers.Add("Authorization", authHeader);
        httpRequest.Headers.Add("Accept", "application/json");

        var client = new HttpClient();
        var httpResponse = await client.SendAsync(httpRequest);

        if (httpResponse.IsSuccessStatusCode)
        {
            string responseJson = await httpResponse.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResponse>(responseJson, new JsonSerializerSettings
            {
                DateFormatString = "dd/MM/yyyy h:mm tt",
            });
        }
        else
        {
            // Do something when request fails
            return default;
        }
    }
}