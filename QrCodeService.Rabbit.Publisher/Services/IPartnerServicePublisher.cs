using RabbitMQ.Interfaces;

namespace QrCodeService.Rabbit.Publisher.Services
{
    public interface IPartnerServicePublisher<in T> where T : IRabbitTask
    {
        Task PublishRequestAsync(T message);

        Task PublishAsync(T message);
    }
}