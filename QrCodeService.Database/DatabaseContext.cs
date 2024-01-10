using Microsoft.EntityFrameworkCore;
using QrCodeService.Database.EntityConfig;
using QrCodeService.Domain.Entity;
using System.Diagnostics.CodeAnalysis;

namespace QrCodeService.Database;

[ExcludeFromCodeCoverage]
public class DatabaseContext : DbContext
{
    public const string DatabaseContextMigrationsHistoryTableName = "MigrationsHistory";

    public const string SchemaName = "public";
    public virtual DbSet<Article> Files { get; set; } = null!;
    public virtual DbSet<Message> Messages { get; set; } = null!;
    public virtual DbSet<HubRoute> HubRouteIds { get; set; } = null!;
    public DatabaseContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(SchemaName);
        modelBuilder.ApplyConfiguration(new FileConfig());
        modelBuilder.ApplyConfiguration(new HubConfig());
        modelBuilder.ApplyConfiguration(new MessageConfig());
    }
}