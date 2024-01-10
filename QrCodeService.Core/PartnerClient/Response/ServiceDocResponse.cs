using Newtonsoft.Json;

namespace QrCodeService.Core.PartnerClient.Response;

public sealed class ServiceDocResponse
{

    [JsonProperty("file")]
    public string? FileId { get; set; }
}