using QrCodeService.Domain.Entity;

namespace QrCodeService.Database.Repositories.Interfaces
{
    public interface IHubRouteRepository
    {
        HubRoute GetTransportInvoiceId(string id);
    }
}