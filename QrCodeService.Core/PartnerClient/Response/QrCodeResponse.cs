using Newtonsoft.Json;


namespace QrCodeService.Core.PartnerClient.Response
{
    public class QrCodeResponse
    {
        [JsonProperty("files")]
        public List<string>? FilesId { get; set; }

    }
}
