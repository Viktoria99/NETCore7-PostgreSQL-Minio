namespace QrCodeService.Core.Services.Interfaces;

public interface IEventProcessingServicesFactory
{
    IEventProcessingService GetEventProcessingService(string code);
}