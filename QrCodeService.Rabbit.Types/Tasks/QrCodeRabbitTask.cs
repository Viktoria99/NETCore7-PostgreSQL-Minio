
namespace QrCodeService.Rabbit.Types.Tasks
{
    public class QrCodeRabbitTask : BaseRabbitTask
    {
        public string? TransportInvoiceId { get; set; }
    }
}