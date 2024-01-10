using Newtonsoft.Json;
using NLog;
using QrCodeService.Core.Services.Interfaces;
using QrCodeService.Database.Repositories.Interfaces;
using QrCodeService.Domain.Entity;
using QrCodeService.Domain.Model;
using QrCodeService.Domain.Types;
using QrCodeService.Rabbit.Types.Tasks;
using RabbitMQ.Interfaces;

namespace QrCodeService.Consumer.Handlers
{
    public class TrafficDocumentHandler : IRabbitService<DocumentRabbitTask>
    {
        private readonly IDocumentFileRepository _documentFileRepository;
        private readonly IPartnerFileService _manageFileService;
        private readonly IPartnerEventService _manageEventService;
        private readonly IMessageRepository _messageRepository;

        private readonly ILogger _logger;
        public TrafficDocumentHandler(
            IDocumentFileRepository documentFileRepository,
            IPartnerFileService manageFileService,
            IPartnerEventService manageEventService,
            IMessageRepository messageRepository
         )
        {
            _documentFileRepository = documentFileRepository;
            _manageFileService = manageFileService;
            _manageEventService = manageEventService;
            _messageRepository = messageRepository;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task ExecuteAsync(DocumentRabbitTask queueMessage, CancellationToken token)
        {

            var message = JsonConvert.SerializeObject(queueMessage.Data);
            _logger.Info($"TrafficDocumentHandler.ExecuteAsync(queueMessage = {message})");
            var trafficDocument = JsonConvert.DeserializeObject<TrafficDocument>(message);
            var fileDto = _documentFileRepository.FindFile(trafficDocument!.ArticleId);
            if (fileDto.OperatorFileId != string.Empty)
            {
                await _manageEventService.PublishEventAsync(TrafficDocumentTypes.ArticleType[trafficDocument.ArticleType!],
                                                         TrafficDocumentTypes.EventType,
                                                         trafficDocument.RouteId);
            }
            var fileResponse = await _manageFileService.SendFileAsync(trafficDocument.UrlXmlFile, trafficDocument.UrlSigFile, trafficDocument.ArticleId);
            var tagId = fileResponse.Files!.First(t => t.Name!.Contains("tag")).Id;
            var fileId = fileResponse.Files!.First(t => t.Name!.Contains("xml")).Id;
            var eventmessage = _manageEventService.GetDocSend(trafficDocument, tagId!, fileId!);
            var eventResponse = await _manageEventService.PostEventAsync(eventmessage);
            var events = eventResponse?.Events;
            var eventsId = events?.Where(v => v.Id != null).Select(s => s.Id).ToList();
            var taskList = new List<Task>();
            foreach (var oneEvent in events!)
            {
                var messageDto = new Message
                {
                    ArticleId = trafficDocument!.ArticleId,
                    ArticleType = trafficDocument.ArticleType,
                    RouteId = trafficDocument.RouteId,
                    Created = DateTimeOffset.UtcNow,
                    Updated = DateTimeOffset.UtcNow,
                    Status = (int)MessageStatus.Send,
                    OperatorMessageId = oneEvent?.Id ?? string.Empty,
                    Data = oneEvent?.Data != null ? JsonConvert.SerializeObject(oneEvent.Data) : string.Empty,
                };

                taskList.Add(_messageRepository.AddMessageAsync(messageDto));
            }
            await Task.WhenAll(taskList);
            await _manageEventService.PublishEventAsync(TrafficDocumentTypes.ArticleType[trafficDocument.ArticleType!],
                                                         TrafficDocumentTypes.EventType,
                                                         trafficDocument.RouteId);
        }


    }
}