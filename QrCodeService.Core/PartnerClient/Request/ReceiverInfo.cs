using Newtonsoft.Json;

namespace QrCodeService.Core.PartnerClient.Request
{
    public class ReceiverInfo
    {
        [JsonProperty("ReceiverId")]
        public string? Id { get; set; }

        [JsonProperty("ReceiverType")]
        public string? TypeName { get; set; }
    }
}