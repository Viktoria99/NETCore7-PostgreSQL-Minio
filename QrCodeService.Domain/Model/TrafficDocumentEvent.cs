using Newtonsoft.Json;
using QrCodeService.Domain.Types;


namespace QrCodeService.Domain.Model
{
    public class TrafficDocumentEvent
    {
        /// <summary>
        /// Error code.
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; }

        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("RouteId")]
        public int? RouteId { get; set; }

        public TrafficDocumentEvent(string? error)
        {
            if (error == string.Empty)
            {
                Code = (int)EventStatus.Success;
            }
            Code = (int)EventStatus.Failed;
        }
    }
}