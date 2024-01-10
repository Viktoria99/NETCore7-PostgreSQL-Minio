using QrCodeService.Core.PartnerClient.Request;
using QrCodeService.Core.PartnerClient.Response;
using QrCodeService.Core.PartnerClient.Response.Confirm;
using QrCodeService.Domain.Model;

namespace QrCodeService.Core.Services.Interfaces
{
    public interface IPartnerEventService
    {
        /// <summary>
        /// Creating an event sending file
        /// </summary>
        /// <param name="docUpload"><paramref name="TrafficDocument"></param>
        /// <param name="tagId">Id file</param>
        /// <param name="fileId">Id received during the upload</param>
        /// <returns><paramref name="PartnerServiceListEvents"></returns>
        PartnerServiceListEvents GetDocSend(TrafficDocument docUpload, string tagId, string fileId);

        PartnerServiceListEvents GetQrCodeRequest(string parentEventId);

        Task<GetEventResponse> GetEventsAsync();

        Task<GetEventResponse> GetEventsUnconfirmedAsync();

        Task PublishEventAsync(string articleType, string actionType, int dolId);

        Task<EventsResponse> PostEventAsync(PartnerServiceListEvents eventValue);
        Task<EventsConfirmResponse> Confirm(ICollection<string> toConfirm);
    }
}