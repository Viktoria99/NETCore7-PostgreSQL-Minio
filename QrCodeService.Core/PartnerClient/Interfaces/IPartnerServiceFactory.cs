
using RemoteApi.Interfaces;

namespace QrCodeService.Core.PartnerClient.Interfaces
{
    public interface IPartnerServiceFactory
    {
        HttpClient GetClient(IHttpClientConfig clientConfig);
    }
}