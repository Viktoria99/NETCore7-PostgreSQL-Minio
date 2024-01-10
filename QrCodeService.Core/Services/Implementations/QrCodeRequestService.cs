using Newtonsoft.Json;
using QrCodeService.Core.Minio;
using QrCodeService.Core.PartnerClient.Response;
using QrCodeService.Core.Services.Interfaces;
using QrCodeService.Database.Repositories.Interfaces;
using QrCodeService.Domain.Types;
using QrCodeService.Rabbit.Publisher.Services;
using QrCodeService.Rabbit.Types.Tasks;


namespace QrCodeService.Core.Services.Implementations
{
    public class QrCodeRequestService : IQrCodeRequestService
    {
        private readonly IHubRouteRepository _hubRepository;
        private readonly IMinioStorage _minioStorage;
        private readonly IPartnerServicePublisher<QrCodeRabbitTask> _partnerServicePublisher;
        private readonly IMessageRepository _messageRepository;
        private readonly IPartnerFileService _partnerServiceFileService;
        public QrCodeRequestService(IHubRouteRepository hubRepository,
                                       IMinioStorage partnerServiceMinio,
                                       IPartnerServicePublisher<QrCodeRabbitTask> qrCodePublisher,
                                       IMessageRepository messageRepository,
                                       IPartnerFileService fileService
                                       )
        {
            _hubRepository = hubRepository;
            _minioStorage = partnerServiceMinio;
            _partnerServicePublisher = qrCodePublisher;
            _messageRepository = messageRepository;
            _partnerServiceFileService = fileService;
        }

        public async Task<byte[]> GetQrCodeAsync(string OperatorMessageId)
        {
            var messageEvent = _messageRepository.SelectMessage(OperatorMessageId);
            var codeResponse = JsonConvert.DeserializeObject<QrCodeResponse>(messageEvent.Result!);
            var fileId = codeResponse!.FilesId!.FirstOrDefault() ?? string.Empty;
            var fileBytes = await _partnerServiceFileService.GetFileAsync(fileId);
            return fileBytes;
        }

        public async Task SaveQrCodeAsync(string operatorId, byte[] dataFile)
        {
            var transportId = _hubRepository.GetTransportInvoiceId(operatorId);
            var messageEvent = _messageRepository.SelectMessage(operatorId);
            var fname = $"{DateTime.Now}-{messageEvent.ArticleId} - {transportId.TransportInvoiceId}.gif";
            var stream = new MemoryStream(dataFile);
            await _minioStorage.SaveFileAsync(stream, fname);
            var eventRabbitTask = new QrCodeRabbitTask
            {
                ObjectType = TrafficDocumentTypes.TransportType,
                ActionType = EventCodes.QrCodeFromOperator,
                TransportInvoiceId = transportId.TransportInvoiceId,
                Data = new
                {
                    transportId.TransportInvoiceId,
                    QrCodeName = fName
                }
            };
            await _partnerServicePublisher.PublishAsync(eventRabbitTask);
        }
    }
}