
using Newtonsoft.Json;

namespace QrCodeService.Core.PartnerClient.Request
{
    public class PartnerServiceEvent
    {
        /// <summary>
        /// Event type code.
        /// </summary>
        [JsonProperty("code")]
        public string? Code { get; set; }
        /// <summary>
        /// Parent.
        /// </summary>
        [JsonProperty("parent")]
        public string? Parent { get; set; }
        /// <summary>
        /// List of service for handle event.
        /// </summary>

        [JsonProperty("consumer")]
        public string[]? Consumer { get; set; }
        /// <summary>
        /// Date of event
        /// </summary>

        [JsonProperty("data")]
        public object? Data { get; set; }
    }
}