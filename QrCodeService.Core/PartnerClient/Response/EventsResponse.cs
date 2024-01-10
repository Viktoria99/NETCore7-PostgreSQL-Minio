
using Newtonsoft.Json;

namespace QrCodeService.Core.PartnerClient.Response
{
    public class EventsResponse
    {
        /// <summary>
        /// State of event handle
        /// </summary>
        [JsonProperty("status")]
        public int? Status { get; set; }
        /// <summary>
        /// Link to the error file
        /// </summary>
        [JsonProperty("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Description of the query result.
        /// </summary>
        [JsonProperty("article")]
        public string? Article { get; set; }

        [JsonProperty("events")]
        public List<EventAttributeResponse>? Events { get; set; }
    }
}