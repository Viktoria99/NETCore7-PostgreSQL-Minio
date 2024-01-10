using RabbitMQ.EventHub.Types;

namespace QrCodeService.Rabbit.Types.Tasks
{
    public class BaseRabbitTask : EventHubRabbitMessage<object>
    {
        public BaseRabbitTask()
        {
            Environment = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? string.Empty;
        }
    }
}