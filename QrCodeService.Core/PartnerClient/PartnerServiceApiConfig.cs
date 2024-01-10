using QrCodeService.Core.PartnerClient.Interfaces;
using RemoteApi.Model.Config;

namespace QrCodeService.Core.PartnerClient
{
    public class PartnerServiceApiConfig : ApiConfig, IPartnerServiceApiConfig
    {
        public required string Secret { get; set; }

        public required string PostFileUrl { get; set; }

        public required string PostEventUrl { get; set; }

        public required string GetEventUrl { get; set; }

        public required string GetEventUnconfirmedUrl { get; set; }

        public required string GetFilesUrl { get; set; }
    }
}