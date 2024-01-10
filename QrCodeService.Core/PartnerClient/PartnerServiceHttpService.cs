using QrCodeService.Core.PartnerClient.Interfaces;
using QrCodeService.Utilities.Extension;

namespace QrCodeService.Core.PartnerClient
{
    public class PartnerServiceHttpService : IPartnerServiceHttpService
    {
        private readonly IPartnerServiceApiConfig _apiConfig;
        private readonly IPartnerServiceFactory _clientFactory;

        public PartnerServiceHttpService(IPartnerServiceApiConfig apiConfig, IPartnerServiceFactory clientFactory)
        {
            _apiConfig = apiConfig;
            _clientFactory = clientFactory;
        }

        public async Task<T> CreateRequestAsync<T>(HttpRequestMessage request, CancellationToken token)
        {
            try
            {
                request.Headers.Add("Content-Transfer-Encoding", "base64");
                request.Headers.Add("X-API-Key", _apiConfig.Secret);
                var client = _clientFactory.GetClient(_apiConfig.Client);
                var httpResponseMessage = await client.SendAsync(request, token);
                var content = await httpResponseMessage.Content.ReadAsStringAsync(token);
                httpResponseMessage.Content.Dispose();
                if (!httpResponseMessage.IsSuccessStatusCode)
                    throw new HttpRequestException($"Error - {request.RequestUri} {httpResponseMessage.StatusCode}", null, httpResponseMessage.StatusCode);

                try
                {
                    return await content.DeserializeJsonAsync<T>();
                }
                catch (Exception e)
                {
                    e.Data.Add($"Response Headers", httpResponseMessage.Headers);
                    e.Data.Add($"Response StatusCode", httpResponseMessage.StatusCode);
                    e.Data.Add($"Response Content", content);
                    throw;
                }
            }
            catch (Exception e)
            {
                e.Data.Add($"Path", request.RequestUri?.ToString());
                e.Data.Add($"Method", request.Method.ToString());
                throw;
            }
        }
    }
}