using NLog;
using QrCodeService.Core.Minio;
using QrCodeService.Core.PartnerClient.Interfaces;
using QrCodeService.Core.PartnerClient.Response;
using QrCodeService.Core.Services.Interfaces;
using QrCodeService.Database.Repositories.Interfaces;
using QrCodeService.Domain.Entity;

namespace QrCodeService.Core.Services
{
    public class PartnerFileService : IPartnerFileService
    {
        private readonly IMinioStorage _minioStorage;
        private readonly IPartnerServiceHttpService _httpService;
        private readonly IPartnerServiceApiConfig _apiConfig;
        private readonly IDocumentFileRepository _documentFileRepository;
        private readonly ILogger _logger;

        public PartnerFileService
            (IMinioStorage minioStorage,
            IPartnerServiceHttpService httpService,
            IPartnerServiceApiConfig apiConfig,
            IDocumentFileRepository fileRepository
            )
        {
            _minioStorage = minioStorage;
            _httpService = httpService;
            _apiConfig = apiConfig;
            _documentFileRepository = fileRepository;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task<PartnerServiceFileResponse> SendFileAsync(string urlXmlFile, string urlSigFile, int articleId)
        {
            try
            {
                _logger.Info($"TrafficDocumentHandler.SendFileAsync(urlXmlFile = {urlXmlFile}, urlSigFile = {urlSigFile}, articleId = {articleId} )");
                var xmlStream = await _minioStorage.GetAsync(urlXmlFile);
                var tagStream = await _minioStorage.GetAsync(urlSigFile);

                var xmlName = urlXmlFile.Split('/').Last();
                var tagName = urlSigFile.Split('/').Last();

                var content = new MultipartFormDataContent();
                content.Add(xmlStream!, "file", xmlName);
                content.Add(tagStream!, "file", tagName);

                var request = new HttpRequestMessage(HttpMethod.Post, _apiConfig.PostFileUrl);
                request.Content = content;
                var response = await _httpService.CreateRequestAsync<PartnerServiceFileResponse>(request);

                var file = new Article
                {
                    Name = urlXmlFile,
                    OperatorFileId = response.Files?.First().Id!,
                    ArticleId = articleId
                };
                await _documentFileRepository.AddFileAsync(file);

                return response;
            }
            catch (Exception messageText)
            {
                _logger.Error(messageText);
                throw;
            }
        }

        public async Task<byte[]> GetFileAsync(string fileUiId)
        {
            var httRequest = new HttpRequestMessage(HttpMethod.Get, $"{_apiConfig.GetFilesUrl}?file_Id={fileUiId}");
            var response = await _httpService.CreateRequestAsync<byte[]>(httRequest);
            return response ?? new byte[] { };
        }

    }
}