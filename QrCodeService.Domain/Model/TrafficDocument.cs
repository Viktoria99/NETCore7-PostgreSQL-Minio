using QrCodeService.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace QrCodeService.Domain.Model
{
    public class TrafficDocument
    {

        public int TransportInvoiceId { get; set; }

        [Required]
        public int ArticleId { get; set; }

        [Required]
        public string? ArticleType { get; set; }

        public int RouteId { get; set; }
        /// <summary>
        /// The address of the request to download the xml file
        /// </summary>
        [Required]
        public string UrlXmlFile { get; set; } = string.Empty;
        /// <summary>
        /// The address of the request to download the tag
        /// </summary>
        [Required]
        public string UrlTagFile { get; set; } = string.Empty;

        public TrafficDocumentSender? Sender { get; set; }

        public List<TrafficDocumentReceiver>? Receivers { get; set; }
    }
}