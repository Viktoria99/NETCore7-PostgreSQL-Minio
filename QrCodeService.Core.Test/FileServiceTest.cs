using Moq;
using QrCodeService.Core.PartnerClient.Interfaces;
using QrCodeService.Core.PartnerClient.Response;
using QrCodeService.Core.Minio;
using QrCodeService.Core.Services;
using QrCodeService.Database.Repositories.Interfaces;
using QrCodeService.Domain.Entity;
using Xunit;

namespace QrCodeService.Core.Test
{
    public class FileServiceTest
    {
        [Theory]
        [InlineData(@"2023/10/13/555/DL_UQCW_.xml", "2023/10/13/555/Dl_UQCW_.tag", 9742)]
        public async void SendFileTest(string urlXmlFile, string urlSigFile, int articleId)
        {
            var mockMinio = new Mock<IMinioStorage>();
            mockMinio.Setup(m => m.GetAsync(It.IsAny<string>())).Returns<Task<StreamContent>>(cont => cont);

            var mockHttpService = new Mock<IPartnerServiceHttpService>();
            mockHttpService.Setup(h => h.CreateRequestAsync<PartnerServiceFileResponse>(It.IsAny<HttpRequestMessage>())).Returns<Task<PartnerServiceFileResponse>>(response => response);

            var mockConfig = new Mock<IPartnerServiceApiConfig>();
            mockConfig.SetupProperty(config => config.PostEventUrl, @"https://******.ru/events");
            mockConfig.SetupProperty(config => config.PostFileUrl, @"https://******.ru/files");

            var mockFile = new Mock<IDocumentFileRepository>();
            mockFile.Setup(f => f.AddFileAsync(It.IsAny<Article>())).Returns<Task>(s => s);

            var _manageFileService = new PartnerFileService(mockMinio.Object, mockHttpService.Object, mockConfig.Object, mockFile.Object);

            var result = await _manageFileService.SendFileAsync(urlXmlFile, urlSigFile, articleId);

        }
    }
}
