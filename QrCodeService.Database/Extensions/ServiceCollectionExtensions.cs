using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QrCodeService.Database.Repositories;
using QrCodeService.Database.Repositories.Interfaces;

namespace QrCodeService.Database.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgreConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        services.AddDbContextPool<DatabaseContext>(optionsAction =>
        {
            optionsAction.EnableSensitiveDataLogging();
            optionsAction.UseNpgsql(connectionString, x =>
            {
                x.MigrationsHistoryTable("MigrationsHistory", DatabaseContext.SchemaName);
                x.SetPostgresVersion(new Version(9, 6));
            });
        });
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IDocumentFileRepository, DocumentFileRepository>();
        services.AddScoped<IHubRouteRepository, HubRouteRepository>();
        return services;
    }
}