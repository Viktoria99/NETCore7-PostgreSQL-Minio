namespace QrCodeService.Minio
{
    public interface IMinioSettings
    {
        string? AccessKey { get; set; }

        string? BusketName { get; set; }

        string? Point { get; set; }

        string? SecretKey { get; set; }
    }
}