using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QrCodeService.Core.PartnerClient;
using QrCodeService.Core.PartnerClient.Interfaces;

namespace QrCodeService.Consumer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            var apiConfig = configuration.GetSection("PartnerApi").Get<PartnerServiceApiConfig>();
            services.AddSingleton<IPartnerServiceApiConfig>(apiConfig!);
            services.AddHttpClient();
            services.AddScoped<IPartnerServiceFactory, PartnerServiceFactory>();
            services.AddScoped<IPartnerServiceHttpService, PartnerServiceHttpService>();

            return services;
        }
    }
}