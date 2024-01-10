using QrCodeService.Rabbit.Types.Tasks;
using RabbitMQ.Models.Config;
using RabbitMQ.Publisher;

namespace QrCodeService.Rabbit.Publisher.Services.Implementation
{
    public class PartnerServiceQrCodePublisher : PartnerServicePublisher<QrCodeRabbitTask>
    {
        public PartnerServiceQrCodePublisher(IRabbitPublisher rabbitPublisher, RabbitSettings settings) : base(rabbitPublisher, settings)
        {
        }

        public override string GetRoutingKey(QrCodeRabbitTask message)
        {
            return $"{ServiceName}.{EventType}.{message.ObjectType}.{message.ActionType}.{message.TransportInvoiceId}.{TraceId}";
        }
    }
}