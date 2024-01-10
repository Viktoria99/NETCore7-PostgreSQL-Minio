
using QrCodeService.Rabbit.Types.Tasks;
using RabbitMQ.Models.Config;
using RabbitMQ.Publisher;

namespace QrCodeService.Rabbit.Publisher.Services.Implementation
{
    public class PartnerServiceDocumentPublisher : PartnerServicePublisher<DocumentRabbitTask>
    {
        public PartnerServiceDocumentPublisher(IRabbitPublisher rabbitPublisher, RabbitSettings settings) : base(rabbitPublisher, settings)
        {
        }

        public override string GetRoutingKey(DocumentRabbitTask message)
        {
            return string.Empty;
        }
    }
}