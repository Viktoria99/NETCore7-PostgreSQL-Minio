using QrCodeService.Core.PartnerClient.Interfaces;
using RemoteApi.Interfaces;
using System.Net.Http.Headers;

namespace QrCodeService.Core.PartnerClient
{
    public class PartnerServiceFactory : IPartnerServiceFactory
    {
        private readonly IHttpClientFactory _clientFactory;
        public PartnerServiceFactory(IHttpClientFactory factory)
        {
            _clientFactory = factory;
        }

        public HttpClient GetClient(IHttpClientConfig clientConfig)
        {
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Connection", "Keep-Alive");
            client.DefaultRequestHeaders.Add("Keep-Alive", "timeout=" + clientConfig.Timeout);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = clientConfig.BaseUri;
            return client;
        }
    }
}