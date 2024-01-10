using Newtonsoft.Json;

namespace QrCodeService.Core.PartnerClient.Request
{
    public class PartnerServiceListEvents
    {
        /// <summary>
        /// List of events for publish.
        /// </summary>

        [JsonProperty("events")]
        public List<PartnerServiceEvent>? Events { get; set; }
    }
}