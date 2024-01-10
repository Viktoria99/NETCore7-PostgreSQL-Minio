using Newtonsoft.Json;
using NLog;
using QrCodeService.Core.PartnerClient.Response;
using QrCodeService.Core.Services.Interfaces;
using QrCodeService.Database.Repositories.Interfaces;
using QrCodeService.Domain.Entity;
using QrCodeService.Domain.Model;
using QrCodeService.Domain.Types;

namespace QrCodeService.Core.Services.Implementations;

public class DocFlowService : IEventProcessingService
{
    private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
    private readonly IMessageRepository _messageRepository;

    public DocFlowService(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    /// <summary>
    /// Save event in the table Message.
    /// </summary>
    /// <param name="partnerEvent"></param>
    /// <returns></returns>
    public async Task ProcessAsync(object partnerEvent)
    {
        _logger.Info("DocFlowService Process");
        var res = (GetEventResponse)partnerEvent;
        var events = res.Events ?? new List<GetEventResponseEvents>();
        var listTaskEvents = new List<Task>();
        foreach (var oneEvent in events)
        {
            var messageData = JsonConvert.SerializeObject(oneEvent.Data);
            var operatorId = oneEvent.Parent ?? string.Empty;
            var messageEvent = _messageRepository.SelectMessage(operatorId);
            var status = messageEvent.OperatorMessageId == null ? (int)MessageStatus.Unknown : (int)MessageStatus.HandleSuccess;
            var messageValue = new Message
            {
                Created = DateTimeOffset.UtcNow,
                Updated = DateTimeOffset.UtcNow,
                Status = status,
                OperatorMessageId = operatorId ?? string.Empty,
                Result = messageData,
                Data = string.Empty
            };
            listTaskEvents.Add(_messageRepository.AddMessageAsync(messageValue));
        }
        await Task.WhenAll(listTaskEvents);
    }

    public async Task SaveDocAsync(TrafficDocument trafficDocument, EventsResponse eventResponse)
    {

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
        var error = eventResponse?.Events?.FirstOrDefault()?.Errors?.FirstOrDefault() ?? string.Empty;
        await _messageRepository.UpdateMessagesStatus(eventsId!, error);
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="partnerEvent"></param>
    /// <returns></returns>
    public async Task ProcessUnconfirmedAsync(object partnerEvent)
    {
        await ProcessAsync(partnerEvent);
    }

}