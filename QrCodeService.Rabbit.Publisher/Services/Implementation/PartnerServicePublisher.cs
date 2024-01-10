using QrCodeService.Utilities.Extension;
using RabbitMQ.Interfaces;
using RabbitMQ.Models.Config;
using RabbitMQ.Publisher;

namespace QrCodeService.Rabbit.Publisher.Services.Implementation
{
    public abstract class PartnerServicePublisher<T> : IPartnerServicePublisher<T> where T : class, IRabbitTask
    {
        private readonly IRabbitPublisher _rabbitPublisher;
        private readonly RabbitSettings _settings;
        protected const string ServiceName = "partner-adapter-server";
        protected const string EventType = "business_action";
        protected const string TraceId = "0";

        public PartnerServicePublisher(IRabbitPublisher rabbitPublisher, RabbitSettings settings)
        {
            _settings = settings;
            _rabbitPublisher = rabbitPublisher;
        }

        public async Task PublishRequestAsync(T message)
        {
            await _rabbitPublisher.Publish(message);
        }

        public async Task PublishAsync(T message)
        {
            var settings = GetQueueSettings();
            var body = await message.SerializeJsonAsync();

            await _rabbitPublisher.Publish(body, settings.ExchangeName, GetRoutingKey(message));
        }

        private PublisherSetting GetQueueSettings()
        {

            var typeMessage = typeof(T);
            if (!_settings.Publishers.ContainsKey(typeMessage))
            {
                throw new ArgumentException($"Not found queue for type message {typeMessage}");
            }
            return _settings.Publishers[typeMessage];
        }

        public abstract string GetRoutingKey(T message);
    }
}