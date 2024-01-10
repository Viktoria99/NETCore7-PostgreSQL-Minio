namespace QrCodeService.Core.Services.Interfaces;

public interface IEventProcessingService
{
    Task ProcessAsync(object partnerEvent);

    Task ProcessUnconfirmedAsync(object partnerEvent);
}