namespace QrCodeService.Core.PartnerClient.Interfaces
{
    public interface IPartnerServiceHttpService
    {
        Task<T> CreateRequestAsync<T>(HttpRequestMessage request);
    }
}