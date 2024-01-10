
namespace QrCodeService.Minio
{
    public class MinioSettings : IMinioSettings
    {
        public string? Point { get; set; }

        public string? AccessKey { get; set; }

        public string? SecretKey { get; set; }

        public string? BusketName { get; set; }
    }
}