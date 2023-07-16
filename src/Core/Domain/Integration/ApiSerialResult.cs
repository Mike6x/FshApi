using Newtonsoft.Json;

namespace FSH.WebApi.Domain.Integration;

public class ApiSerialResult
{
    [JsonProperty(PropertyName = "type")]
    public string Type { get; set; } = default!;

    [JsonProperty(PropertyName = "status")]
    public bool Status { get; set; }

    [JsonProperty(PropertyName = "message")]
    public string Message { get; set; } = default!;

    [JsonProperty(PropertyName = "totalRecord")]
    public int TotalRecord { get; set; }

    [JsonProperty(PropertyName = "records")]
    public List<ApiSerial> Records { get; set; } = default!;
}
