using Moq;
using QrCodeService.Core.Minio;
using QrCodeService.Core.PartnerClient.Interfaces;
using QrCodeService.Core.PartnerClient.Request;
using QrCodeService.Core.PartnerClient.Response;
using QrCodeService.Core.Services;
using QrCodeService.Core.Services.Interfaces;
using QrCodeService.Domain.Types;
using QrCodeService.Rabbit.Publisher.Services.Implementation;
using RabbitMQ.Models.Config;
using RabbitMQ.Publisher;
using Xunit;

namespace QrCodeService.Core.Test
{
    public class EventServiceTest
    {

        private IPartnerEventService _manageEventService;

        [Theory]
        [InlineData("GH", "SenderTest", "Test", "46486256935-1646", "712588", "123456")]
        [InlineData("OPL", "Sender", "Receiver", "4521-1258-1287", "12579", "00000")]

        public async void PostEventAsyncTest(string articleType, string senderType, string receiverType, string subscriberId, string fileId, string tagId)
        {
            CreateServices();
            var eventObj = new PartnerServiceListEvents
            {
                Events = new List<PartnerServiceEvent> {
                    new PartnerServiceEvent
                        {
                            Code = EventFields.EventCode,
                            Consumer = new string[] { EventFields.EventConsumerType},
                            Data = new DocSend
                            {
                                SubscriberId = subscriberId,
                                SubscriberName = senderType,
                                DoType = TrafficDocumentTypes.DocFlowType,
                                Document = new TransportDocument
                                {
                                    FileId = fileId,
                                    DocumentType = articleType,
                                    Tags = new List<string>{ tagId }
                                },
                                Receivers = new List<ReceiverInfo>{
                                            new ReceiverInfo {
                                                Id = subscriberId,
                                                TypeName = receiverType
                                                }
                                            }
                            }
                        }
                }
            };
            await _manageEventService.PostEventAsync(eventObj);
        }

        public void CreateServices()
        {
            var mockRabbitPublisher = new Mock<IRabbitPublisher>();
            var mockRabbitSettings = new Mock<RabbitSettings>();

            var eventPublisher = new PartnerServiceEventPublisher(mockRabbitPublisher.Object, mockRabbitSettings.Object);

            var mockMinio = new Mock<IMinioStorage>();
            mockMinio.Setup(m => m.GetAsync(It.IsAny<string>())).Returns<Task<StreamContent>>(cont => cont);

            var mockHttpService = new Mock<IPartnerServiceHttpService>();
            mockHttpService.Setup(h => h.CreateRequestAsync<PartnerServiceFileResponse>(It.IsAny<HttpRequestMessage>())).Returns<Task<PartnerServiceFileResponse>>(response => response);

            var mockConfig = new Mock<IPartnerServiceApiConfig>();
            mockConfig.SetupProperty(config => config.PostEventUrl, @"https://******.ru/events");

            _manageEventService = new PartnerEventService(mockHttpService.Object, mockConfig.Object, eventPublisher);

        }
    }
}