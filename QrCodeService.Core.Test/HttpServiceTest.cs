using Microsoft.Extensions.DependencyInjection;
using QrCodeService.Core.PartnerClient;
using QrCodeService.Core.PartnerClient.Interfaces;
using RemoteApi.Model.Config;
using Xunit;

namespace QrCodeService.Core.Test
{
    /// <summary>
    /// Check partner's test stand
    /// </summary>
    public class HttpServiceTest
    {
        private IPartnerServiceApiConfig _apiConfig;
        private IPartnerServiceFactory _clientFactory;

        [Fact]
        public void GetClientTest()
        {
            CreateServices();
            var res = _clientFactory.GetClient(_apiConfig.Client);
            Assert.NotNull(res);
        }

        public void CreateServices()
        {
            var services = new ServiceCollection();
            var apiConfig = new PartnerServiceApiConfig
            {
                Client = new HttpClientConfig(new Uri(@"https://*******.ru"), 60),
                Secret = "f38********************12",
                PostFileUrl = @"https://*****.ru/files",
                PostEventUrl = @"https://*****.ru/events",
                GetEventUrl = @"https://****.ru/events",
                GetEventUnconfirmedUrl = @"https://******.ru/events/unconfirmed",
                GetFilesUrl = @""
            };
            services.AddSingleton<IPartnerServiceApiConfig>(apiConfig!);
            services.AddSingleton<IPartnerServiceHttpService, PartnerServiceHttpService>();
            services.AddScoped<IPartnerServiceFactory, PartnerServiceFactory>();
            services.AddHttpClient();
            var serviceProvider = services.BuildServiceProvider();

            _apiConfig = serviceProvider.GetService<IPartnerServiceApiConfig>();
            _clientFactory = serviceProvider.GetService<IPartnerServiceFactory>();
        }
    }
}