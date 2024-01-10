using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QrCodeService.Domain.Entity;


namespace QrCodeService.Database.EntityConfig
{
    public sealed class HubConfig : IEntityTypeConfiguration<HubRoute>
    {
        public void Configure(EntityTypeBuilder<HubRoute> builder)
        {
            builder.ToTable("HubRouteIds")
                .ToTable(t => t.HasComment("IDs issued by HUB "));
            builder.Property(p => p.Id)
                .HasComment("Record ID");
            builder.Property(p => p.Updated)
                .HasComment("Update time");
            builder.Property(p => p.Created)
                .HasComment("Creation time");
            builder.Property(p => p.RouteId)
                .HasComment("Route ID");
            builder.Property(p => p.TransportInvoiceId)
                .HasComment("Transport Invoice");
        }
    }
}