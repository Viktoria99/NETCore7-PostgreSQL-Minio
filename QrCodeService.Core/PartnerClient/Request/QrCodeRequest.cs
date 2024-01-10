using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace QrCodeService.Core.PartnerClient.Request
{
    public class QrCodeRequest
    {
        [JsonProperty("Id")]
        [Required]
        public string? Id { get; set; }
    }
}
