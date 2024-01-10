
namespace QrCodeService.Core.Minio
{
    public interface IMinioStorage
    {
        Task<StreamContent?> GetAsync(string path);
        Task SaveFileAsync(MemoryStream stream, string fName);

    }
}