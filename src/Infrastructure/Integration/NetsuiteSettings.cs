namespace FSH.WebApi.Infrastructure.Integration;
public class NetsuiteSettings
{
    public string AccountId { get; set; } = default!;

    public string ClientId { get; set; } = default!;
    public string ClientSecret { get; set; } = default!;

    public string TokenId { get; set; } = default!;
    public string TokenSecret { get; set; } = default!;

    public string Realm { get; set; } = default!;

    public string ScriptId { get; set; } = default!;
    public string DeploymentId { get; set; } = default!;
}
