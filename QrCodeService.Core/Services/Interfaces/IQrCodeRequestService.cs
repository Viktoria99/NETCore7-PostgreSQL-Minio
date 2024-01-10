namespace QrCodeService.Core.Services.Interfaces
{
    public interface IQrCodeRequestService
    {
        Task SaveQrCodeAsync(string operatorId, byte[] dataFile);

        Task<byte[]> GetQrCodeAsync(string OperatorMessageId);
    }
}