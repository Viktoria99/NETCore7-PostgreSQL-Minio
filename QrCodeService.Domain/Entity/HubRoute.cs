using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QrCodeService.Domain.Entity
{
    /// <summary>
    /// The route given by the hub
    /// </summary>
    public class HubRoute
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTimeOffset Created { get; set; }

        [Required]
        public DateTimeOffset Updated { get; set; }

        [Required]
        [MaxLength(100)]
        public string RouteId { get; set; } = null!;

        [MaxLength(100)]
        public string TransportInvoiceId { get; set; } = null!;
    }
}