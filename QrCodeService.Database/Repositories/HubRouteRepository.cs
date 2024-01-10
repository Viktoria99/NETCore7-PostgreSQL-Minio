using QrCodeService.Database.Repositories.Interfaces;
using QrCodeService.Domain.Entity;

namespace QrCodeService.Database.Repositories
{
    public class HubRouteRepository : IHubRouteRepository
    {
        protected readonly DatabaseContext _context;

        public HubRouteRepository(DatabaseContext context)
        {
            _context = context;
        }

        public HubRoute GetTransportInvoiceId(string id)
        {
            var gis = _context.HubRouteIds.FirstOrDefault(s => s.TransportInvoiceId == id);
            return gis ?? new HubRoute();
        }
    }
}
