
using System.ComponentModel.DataAnnotations;

namespace QrCodeService.Domain.Model
{
    public class TrafficDocumentSender
    {
        [Required]
        public string? Id { get; set; }

        [Required]
        public string? Type { get; set; }
    }
}