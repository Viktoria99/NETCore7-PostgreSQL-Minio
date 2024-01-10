using Minio;
using Minio.DataModel.Args;
using QrCodeService.Minio;

namespace QrCodeService.Core.Minio
{
    public class MinioStorage : IMinioStorage
    {
        private readonly IMinioClient _minioClient;
        private readonly IMinioSettings _minioSettings;
        public MinioStorage(IMinioClient minioClient, IMinioSettings minioConfig)
        {
            _minioClient = minioClient;
            _minioSettings = minioConfig;
        }

        public async Task<StreamContent?> GetAsync(string path)
        {
            StreamContent? file = null;
            var args = new GetObjectArgs()
               .WithBucket(_minioSettings.BusketName)
               .WithObject(path)
               .WithCallbackStream((stream) =>
               {
                   file = new StreamContent(stream);
               });

            await _minioClient.GetObjectAsync(args);
            return file;
        }

        public async Task SaveFileAsync(MemoryStream stream, string fName)
        {
            var args = new PutObjectArgs()
                .WithBucket(_minioSettings.BusketName)
                .WithObject($"{fName}")
                .WithStreamData(stream)
                .WithObjectSize(stream.Length)
                .WithContentType("text/plain");

            await _minioClient.PutObjectAsync(args);
        }

    }
}