
using Newtonsoft.Json;

namespace QrCodeService.Core.PartnerClient.Response
{
    public class EventAttributeResponse
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("errors")]
        public string[]? Errors { get; set; }
        /// <summary>
        /// Code of event
        /// </summary>
        [JsonProperty("code")]
        public string? Code { get; set; }
        /// <summary>
        /// Number of producer
        /// </summary>
        [JsonProperty("producer")]
        public string? Producer { get; set; }
        /// <summary>
        /// Number of consumer
        /// </summary>
        [JsonProperty("consumer")]
        public string[]? Consumer { get; set; }
        /// <summary>
        /// Date of event create
        /// </summary>
        [JsonProperty("created")]
        public string? Created { get; set; }
        /// <summary>
        /// Parameters for handle event
        /// </summary>
        [JsonProperty("data")]
        public object? Data { get; set; }
    }
}