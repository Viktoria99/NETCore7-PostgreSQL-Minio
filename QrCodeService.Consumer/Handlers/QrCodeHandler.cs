using Newtonsoft.Json;
using NLog;
using QrCodeService.Core.Services.Interfaces;
using QrCodeService.Domain.Model;
using QrCodeService.Rabbit.Types.Tasks;
using System.Text;

namespace QrCodeService.Consumer.Handlers
{
    public class QrCodeHandler : IRabbitService<QrCodeRabbitTask>
    {
        private readonly IPartnerEventService _eventService;
        private readonly IQrCodeRequestService _qrCodeRequestService;

        private readonly ILogger _logger;
        public QrCodeHandler(IPartnerEventService eventService, IQrCodeRequestService qrCodeRequestService)
        {

            _eventService = eventService;
            _qrCodeRequestService = qrCodeRequestService;
            _logger = LogManager.GetCurrentClassLogger();
        }
        public async Task ExecuteAsync(QrCodeRabbitTask queueMessage, CancellationToken token)
        {
            var message = JsonConvert.SerializeObject(queueMessage.Data);
            _logger.Info($"QrCodeHandler.ExecuteAsync(queueMessage = {message})");

            var documentId = JsonConvert.DeserializeObject<QrCode>(message);
            var Id = documentId!.Id ?? string.Empty;

            var qrCodeRequest = _eventService.GetQrCodeRequest(Id);
            var response = await _eventService.PostEventAsync(qrCodeRequest);

            var errorlist = response!.Events!.FirstOrDefault()!.Errors ?? new string[] { };

            if (errorlist.Length > 0)
            {
                var errors = new StringBuilder().AppendJoin(';', errorlist).ToString();
                _logger.Error($"QrCodeHandler.ExecuteAsync Error = [{errors}]");
            }
            var fileBytes = await _qrCodeRequestService.GetQrCodeAsync(Id);

            await _qrCodeRequestService.SaveQrCodeAsync(Id, fileBytes);

            var ListId = new List<string>() { Id };
            var confirmResponse = await _eventService.Confirm(ListId);
            var confirmMessage = JsonConvert.SerializeObject(confirmResponse);
            _logger.Info($"QrCodeHandler.ExecuteAsync Confirm = {confirmMessage})");

        }
    }
}
