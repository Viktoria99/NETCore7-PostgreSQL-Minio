using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using QrCodeService.Core.Minio;

namespace QrCodeService.Minio.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMinioConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var minioSettings = configuration.GetSection("MinioSettings").Get<MinioSettings>();
            services.AddSingleton<IMinioSettings>(minioSettings!);
            services.AddMinio(configureClient => configureClient
                    .WithEndpoint(minioSettings?.Point)
                    .WithCredentials(minioSettings?.AccessKey, minioSettings?.SecretKey)
                    .WithSSL(false));
            services.AddScoped<IMinioStorage, MinioStorage>();
            return services;
        }
    }
}