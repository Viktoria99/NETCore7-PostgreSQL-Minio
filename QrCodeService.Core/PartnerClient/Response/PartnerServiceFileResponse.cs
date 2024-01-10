using Newtonsoft.Json;

namespace QrCodeService.Core.PartnerClient.Response
{
    public class PartnerServiceFileResponse
    {

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("article")]
        public string Article { get; set; } = string.Empty;

        [JsonProperty("files")]
        public List<PartnerServiceFile>? Files { get; set; }
    }
}