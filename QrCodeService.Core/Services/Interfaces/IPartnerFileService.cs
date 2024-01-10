using QrCodeService.Core.PartnerClient.Response;

namespace QrCodeService.Core.Services.Interfaces
{
    public interface IPartnerFileService
    {
        /// <summary>
        /// Send file with transport invoice
        /// </summary>
        /// <param name="urlXmlFile">Path for downloading xml from the Minio repository</param>
        /// <param name="urlTagFile">Path for downloading tag from the Minio repository</param>
        /// <param name="articleId">The ID of the bill of lading received from the file <paramref name="UploadDocument"></param>
        /// <returns><paramref name="PartnerServiceFileResponse"></returns>
        Task<PartnerServiceFileResponse> SendFileAsync(string urlXmlFile, string urlSigFile, int articleId);

        Task<byte[]> GetFileAsync(string fileUiId);
    }
}