
using System.ComponentModel.DataAnnotations;

namespace QrCodeService.Domain.Model
{
    public class TrafficDocumentReceiver
    {
        [Required]
        public string? Id { get; set; }

        public string? Type { get; set; }
    }
}