using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;


namespace QrCodeService.Core.PartnerClient.Request
{
    public class ServiceDocRequest
    {
        [JsonProperty("DocumentType")]
        [Required]
        public string? DocumentType { get; set; }

        [JsonProperty("Comment")]
        [Required]
        public string? Comment { get; set; }

        [JsonProperty("CertificateId")]
        [Required]
        public string? SignatoryCertificateId { get; set; }

    }
}
