using RemoteApi.Interfaces;

namespace QrCodeService.Core.PartnerClient.Interfaces
{
    public interface IPartnerServiceApiConfig : IApiConfig
    {
        string Secret { get; set; }

        string PostFileUrl { get; set; }

        string PostEventUrl { get; set; }

        public string GetEventUrl { get; set; }

        public string GetEventUnconfirmedUrl { get; set; }

        public string GetFilesUrl { get; set; }

    }
}