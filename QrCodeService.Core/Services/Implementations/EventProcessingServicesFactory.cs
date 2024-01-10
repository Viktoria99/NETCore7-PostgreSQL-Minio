using QrCodeService.Core.Services.Interfaces;
using QrCodeService.Domain.Types;


namespace QrCodeService.Core.Services.Implementations;

public class EventProcessingServicesFactory : IEventProcessingServicesFactory
{
    private readonly ICollection<IEventProcessingService> _services;

    public EventProcessingServicesFactory(IEnumerable<IEventProcessingService> services)
    {
        _services = services.ToArray();
    }

    public IEventProcessingService GetEventProcessingService(string code)
    {
        return code switch
        {
            EventCodes.NewResponse => GetService<ResponseService>(),
            EventCodes.DocFlow => GetService<DocFlowService>(),
            EventCodes.ServiceDocResponse => GetService<DocResponseService>(),
            _ =>
                throw new ArgumentOutOfRangeException(code, "Unknown event code")
        };
    }

    private IEventProcessingService GetService<T>() where T : IEventProcessingService
    {
        return _services.Single(x => x is T);
    }
}