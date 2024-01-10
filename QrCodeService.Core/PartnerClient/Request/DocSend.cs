using Newtonsoft.Json;

namespace QrCodeService.Core.PartnerClient.Request
{
    public class DocSend
    {
        [JsonProperty("customerId")]
        public string? SubscriberId { get; set; }

        [JsonProperty("typeCusotmer")]
        public string? SubscriberName { get; set; }

        [JsonProperty("typeDo")]
        public string? DoType { get; set; }

        [JsonProperty("receiver")]
        public List<ReceiverInfo>? Receivers { get; set; }

        [JsonProperty("document")]
        public TransportDocument? Document { get; set; }
    }
}