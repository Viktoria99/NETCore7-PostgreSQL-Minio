using Newtonsoft.Json;
using NLog;
using QrCodeService.Core.PartnerClient.Interfaces;
using QrCodeService.Core.PartnerClient.Request;
using QrCodeService.Core.PartnerClient.Response;
using QrCodeService.Core.PartnerClient.Response.Confirm;
using QrCodeService.Core.Services.Interfaces;
using QrCodeService.Domain.Model;
using QrCodeService.Domain.Types;
using QrCodeService.Rabbit.Publisher.Services;
using QrCodeService.Rabbit.Types.Tasks;

using System.Text;

namespace QrCodeService.Core.Services
{
    public class PartnerEventService : IPartnerEventService
    {
        private readonly IPartnerServiceHttpService _httpService;
        private readonly IPartnerServiceApiConfig _apiConfig;
        private readonly IPartnerServicePublisher<EventRabbitTask> _eventPublisher;
        public PartnerEventService(IPartnerServiceHttpService httpService,
                                        IPartnerServiceApiConfig apiConfig,
                                        IPartnerServicePublisher<EventRabbitTask> partnerPublisher)
        {
            _httpService = httpService;
            _apiConfig = apiConfig;
            _eventPublisher = partnerPublisher;

        }

        public PartnerServiceListEvents GetDocSend(TrafficDocument trafficDoc, string tagId, string fileId)
        {
            var request = new PartnerServiceListEvents
            {
                Events = new List<PartnerServiceEvent> {
                    new PartnerServiceEvent
                        {
                            Code = EventFields.EventCode,
                            Consumer = new string[] {EventFields.EventConsumerType},
                            Data = new DocSend
                            {
                                SubscriberId = trafficDoc.Sender?.Id,
                                SubscriberName = trafficDoc.Sender?.Type,
                                DoType = TrafficDocumentTypes.DocFlowType,
                                Document = new TransportDocument
                                {
                                    FileId = fileId,
                                    DocumentType = trafficDoc.ArticleType,
                                    Tags = new List<string>{ tagId!}
                                },
                                Receivers = trafficDoc.Receivers?.Select(r =>
                                            new ReceiverInfo {
                                                Id = r.Id,
                                                TypeName = r.Type
                                            }).ToList()
                            }
                        }
                }
            };
            return request;
        }

        public async Task PublishEventAsync(string articleType, string actionType, int dolId)
        {

            var eventTask = new EventRabbitTask
            {
                ObjectType = articleType,
                ActionType = actionType,
                Data = new TrafficDocumentEvent(string.Empty)
                {
                    RouteId = dolId,
                    Message = string.Empty
                }
            };
            await _eventPublisher.PublishAsync(eventTask);
        }

        public async Task<GetEventResponse> GetEventsAsync()
        {
            var eventRequest = new HttpRequestMessage(HttpMethod.Get, _apiConfig.GetEventUrl);

            var content = new MultipartFormDataContent();
            content.Add(new StringContent("timeout"), "60");
            content.Add(new StringContent("limit"), "10");
            eventRequest.Content = new StringContent("", Encoding.UTF8, "application/json");
            return await _httpService.CreateRequestAsync<GetEventResponse>(eventRequest);
        }

        public async Task<GetEventResponse> GetEventsUnconfirmedAsync()
        {
            var eventRequest = new HttpRequestMessage(HttpMethod.Get, $"{_apiConfig.GetEventUnconfirmedUrl}?limit=100");
            var res = await _httpService.CreateRequestAsync<GetEventResponse>(eventRequest);
            return res;
        }

        public async Task<EventsResponse> PostEventAsync(PartnerServiceListEvents eventValue)
        {
            var eventJson = JsonConvert.SerializeObject(eventValue);
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, _apiConfig.PostEventUrl);
            httpRequest.Content = new StringContent(eventJson, Encoding.UTF8, "application/json");
            var eventResponse = await _httpService.CreateRequestAsync<EventsResponse>(httpRequest);
            return eventResponse;
        }

        public PartnerServiceListEvents GetQrCodeRequest(string eventId)
        {
            var eventRequest = new PartnerServiceEvent
            {
                Code = EventCodes.QrCodeRequest,
                Consumer = new string[] { EventFields.EventConsumerType },
                Data = new QrCodeRequest
                {
                    Id = eventId
                }
            };
            var eventList = new PartnerServiceListEvents
            {
                Events = new List<PartnerServiceEvent> {
                    eventRequest
                }
            };

            return eventList;
        }

        public Task<EventsConfirmResponse> Confirm(ICollection<string> toConfirm)
        {
            return Task.FromResult(new EventsConfirmResponse());
        }



    }
}