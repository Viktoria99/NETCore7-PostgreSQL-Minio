using Newtonsoft.Json;

namespace QrCodeService.Core.PartnerClient.Response.Confirm
{
    public class EventsConfirmResponse
    {
        /// <summary>
        /// Status of response
        /// </summary>
        [JsonProperty("status")]
        public int? Status { get; set; }

        /// <summary>
        /// Type or error
        /// </summary>
        [JsonProperty("type")]

        public string? Type { get; set; }

        [JsonProperty("article")]
        public string? Article { get; set; }

        /// <summary>
        /// Count of confirm messages
        /// </summary>
        [JsonProperty("count")]
        public int? Count { get; set; }

    }
}
