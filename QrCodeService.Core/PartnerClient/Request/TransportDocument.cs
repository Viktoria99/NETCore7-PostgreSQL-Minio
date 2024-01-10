using Newtonsoft.Json;

namespace QrCodeService.Core.PartnerClient.Request
{
    public class TransportDocument
    {
        [JsonProperty("File")]
        public string? FileId { get; set; }

        [JsonProperty("DocumentType")]
        public string? DocumentType { get; set; }

        [JsonProperty("Comments")]
        public string? Comments { get; set; } = string.Empty;

        [JsonProperty("Tags")]
        public List<string>? Tags { get; set; }

    }
}