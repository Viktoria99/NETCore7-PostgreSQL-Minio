using NLog;
using QrCodeService.Core.Services.Interfaces;

namespace QrCodeService.Core.Services.Implementations;

public class ResponseService : IEventProcessingService
{
    private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="partnerServiceEvent"></param>
    /// <returns></returns>
    public Task ProcessAsync(object partnerServiceEvent)
    {
        _logger.Info("ResponseService Process");
        return Task.CompletedTask;
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="partnerServiceEvent"></param>
    /// <returns></returns>
    public Task ProcessUnconfirmed(object partnerServiceEvent)
    {
        _logger.Info("ResponseService Process Unconfirmed");
        return Task.CompletedTask;
    }

    public Task ProcessUnconfirmedAsync(object partnerServiceEvent)
    {
        throw new NotImplementedException();
    }
}