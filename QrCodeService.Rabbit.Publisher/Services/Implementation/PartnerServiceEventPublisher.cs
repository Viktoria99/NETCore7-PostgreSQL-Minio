using QrCodeService.Rabbit.Types.Tasks;
using RabbitMQ.Models.Config;
using RabbitMQ.Publisher;

namespace QrCodeService.Rabbit.Publisher.Services.Implementation
{
    public class PartnerServiceEventPublisher : PartnerServicePublisher<EventRabbitTask>
    {

        public PartnerServiceEventPublisher(IRabbitPublisher rabbitPublisher, RabbitSettings settings) : base(rabbitPublisher, settings)
        {


        }

        public override string GetRoutingKey(EventRabbitTask message)
        {
            return $"{ServiceName}.{EventType}.{message.ObjectType}.{message.ActionType}.{message.ArticleId}.{TraceId}";
        }
    }
}